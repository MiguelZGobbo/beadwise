using System.Diagnostics;
using BeadWise.ControlledProcessFixture;
using BeadWise.Core.Contracts;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Windows.Processes;

namespace BeadWise.Windows.Tests.Fixtures;

internal sealed class ControlledProcessFixture : IAsyncDisposable
{
    private const string FixtureModeArgument = "--beadwise-controlled-fixture";
    private const string FixtureKeyArgument = "--fixture-key";
    private const string MaximumLifetimeArgument = "--max-lifetime-ms";
    private const string SuppressReadyArgument = "--suppress-ready";
    private const string ReadyPrefix = "BEADWISE_FIXTURE_READY";
    private const string ShutdownPrefix = "BEADWISE_FIXTURE_SHUTDOWN";
    private static readonly TimeSpan ShutdownTimeout = TimeSpan.FromSeconds(5);
    private readonly Process _process;
    private readonly object _recoveryLock = new();
    private readonly FixtureDisposalCoordinator _disposal;
    private ControlledFixtureRecovery? _recovery;
    private volatile bool _disposed;

    private ControlledProcessFixture(
        Process process,
        Guid fixtureKey,
        ProcessIdentity identity)
    {
        _process = process;
        FixtureKey = fixtureKey;
        Identity = identity;
        _disposal = new FixtureDisposalCoordinator(_recoveryLock,
            () => _recovery?.DisposeAsync().AsTask() ?? Task.CompletedTask,
            () => StopProcessAsync(_process, FixtureKey),
            _process.Dispose);
    }

    public Guid FixtureKey { get; }

    public int ProcessId => Identity.ProcessId;

    public ProcessIdentity Identity { get; }

    public bool IsRunning => !_disposed && !_process.HasExited;

    internal IControlledProcessStateSource CreateStateSource(ChangePlan approvedPlan) =>
        new ControlledProcessFixtureStateSource(
            ControlledProcessTarget.FromOwnedFixture(_process, FixtureKey, Identity),
            FixtureKey,
            approvedPlan);

    internal IControlledProcessActionExecutor CreateActionExecutor(ChangePlan approvedPlan) =>
        new ControlledProcessFixtureActionExecutor(
            ControlledProcessTarget.FromOwnedFixture(
                _process,
                FixtureKey,
                Identity),
            FixtureKey,
            approvedPlan);

    internal ControlledProcessTarget CreateTarget() =>
        ControlledProcessTarget.FromOwnedFixture(_process, FixtureKey, Identity);

    internal ControlledFixtureRecovery CreateRecovery(ChangePlan approvedPlan) =>
        ControlledFixtureRecovery.ForOwner(this, approvedPlan);

    internal ControlledFixtureRecovery GetOrCreateRecovery(
        ChangePlan approvedPlan, Func<ControlledFixtureRecovery> create)
    {
        ArgumentNullException.ThrowIfNull(approvedPlan);
        lock (_recoveryLock)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (_recovery is not null)
            {
                if (!_recovery.IsBoundTo(this, approvedPlan))
                    throw new InvalidOperationException("The fixture already has a different recovery plan.");
                return _recovery;
            }

            var recovery = create();
            if (!recovery.IsBoundTo(this, approvedPlan))
                throw new InvalidOperationException("The recovery context does not belong to this fixture and plan.");
            return _recovery = recovery;
        }
    }

    public static Task<ControlledProcessFixture> StartAsync() =>
        StartAsync(new ControlledProcessFixtureOptions());

    public static Task<ControlledProcessFixture> StartAsync(
        ControlledProcessFixtureOptions options,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.StartupTimeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(options));
        }
        var maximumLifetimeMilliseconds = options.MaximumLifetime.TotalMilliseconds;
        if (maximumLifetimeMilliseconds < 1
            || maximumLifetimeMilliseconds > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(options));
        }

        cancellationToken.ThrowIfCancellationRequested();
        return StartCoreAsync(options, (int)maximumLifetimeMilliseconds, cancellationToken);
    }

    internal static bool MatchesReadySignal(
        string? readyLine,
        Guid expectedFixtureKey,
        int expectedProcessId)
    {
        var fields = readyLine?.Split('|');
        return fields is [ReadyPrefix, var fixtureKeyText, var processIdText]
            && Guid.TryParseExact(fixtureKeyText, "D", out var fixtureKey)
            && fixtureKey == expectedFixtureKey
            && int.TryParse(processIdText, out var processId)
            && processId == expectedProcessId;
    }

    private static async Task<ControlledProcessFixture> StartCoreAsync(
        ControlledProcessFixtureOptions options,
        int maximumLifetimeMilliseconds,
        CancellationToken cancellationToken)
    {
        var fixtureKey = Guid.NewGuid();
        var executablePath = Path.ChangeExtension(
            typeof(ControlledProcessFixtureHost).Assembly.Location,
            ".exe");

        if (!File.Exists(executablePath))
        {
            throw new FileNotFoundException(
                "The controlled process fixture executable was not copied to the test output.",
                executablePath);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        startInfo.ArgumentList.Add(FixtureModeArgument);
        startInfo.ArgumentList.Add(FixtureKeyArgument);
        startInfo.ArgumentList.Add(fixtureKey.ToString("D"));
        startInfo.ArgumentList.Add(MaximumLifetimeArgument);
        startInfo.ArgumentList.Add(
            maximumLifetimeMilliseconds.ToString(
                System.Globalization.CultureInfo.InvariantCulture));
        if (options.SuppressReadySignal)
        {
            startInfo.ArgumentList.Add(SuppressReadyArgument);
        }

        var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("The controlled process fixture did not start.");

        try
        {
            var readyLine = await process.StandardOutput.ReadLineAsync(cancellationToken)
                .AsTask().WaitAsync(options.StartupTimeout, cancellationToken);
            ValidateReadyLine(readyLine, fixtureKey, process.Id);

            if (process.HasExited)
            {
                throw new InvalidOperationException(
                    "The controlled process fixture exited before its identity was captured.");
            }

            var creationTime = new DateTimeOffset(process.StartTime.ToUniversalTime());
            var identity = new ProcessIdentity(process.Id, creationTime);
            return new ControlledProcessFixture(process, fixtureKey, identity);
        }
        catch (Exception startupFailure)
        {
            var ownershipTransferred = false;
            try
            {
                if (options.StartupFailureCleanup is not null)
                {
                    await options.StartupFailureCleanup();
                }
                else
                {
                    await StopProcessAsync(process, fixtureKey);
                }
                if (!HasExited(process))
                    throw new InvalidOperationException("Startup cleanup did not confirm the fixture exited.");
            }
            catch (Exception cleanupFailure)
            {
                startupFailure.Data["BeadWise.ControlledProcessFixture.CleanupFailure"] =
                    cleanupFailure;
                if (options.RetainStartupCleanup is not null)
                {
                    options.RetainStartupCleanup(CreatePendingCleanup(process, fixtureKey));
                    ownershipTransferred = true;
                }
                else
                {
                    ownershipTransferred = true;
                    _ = ReleaseAfterExitAsync(process);
                }
            }
            finally
            {
                if (!ownershipTransferred) process.Dispose();
            }

            throw;
        }
    }

    public ValueTask DisposeAsync()
    {
        lock (_recoveryLock)
        {
            _disposed = true;
            return new ValueTask(_disposal.DisposeAsync());
        }
    }

    private static void ValidateReadyLine(
        string? readyLine,
        Guid expectedFixtureKey,
        int expectedProcessId)
    {
        if (!MatchesReadySignal(readyLine, expectedFixtureKey, expectedProcessId))
        {
            throw new InvalidOperationException(
                "The controlled process fixture returned an invalid READY signal.");
        }
    }

    private static async Task StopProcessAsync(Process process, Guid fixtureKey)
    {
        if (process.HasExited)
        {
            return;
        }

        try
        {
            await process.StandardInput.WriteLineAsync(
                $"{ShutdownPrefix}|{fixtureKey:D}");
            await process.StandardInput.FlushAsync();
        }
        catch (Exception shutdownFailure)
            when (IsClosedPipeFailure(shutdownFailure) && HasExited(process))
        {
            return;
        }

        await process.WaitForExitAsync().WaitAsync(ShutdownTimeout);
    }

    private static PendingFixtureCleanup CreatePendingCleanup(Process process, Guid fixtureKey)
    {
        ProcessIdentity? identity = null;
        try
        {
            if (!HasExited(process))
                identity = new ProcessIdentity(process.Id, new DateTimeOffset(process.StartTime.ToUniversalTime()));
        }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            // An unavailable identity cannot authorize a forced stop.
        }

        return new PendingFixtureCleanup(
            () => HasExited(process),
            () => StopProcessAsync(process, fixtureKey),
            async () =>
            {
                if (identity is null) throw new InvalidOperationException("The launch identity is unavailable.");
                var target = ControlledProcessTarget.FromOwnedFixture(process, fixtureKey, identity.Value);
                if (!target.IsOwnedLaunchContext()) throw new InvalidOperationException("Fixture ownership was lost.");
                if (!target.IsRunning()) return;
                if (target.ReadIdentity() != identity.Value) throw new InvalidOperationException("Fixture identity changed.");
                target.Kill();
                await process.WaitForExitAsync().WaitAsync(ShutdownTimeout);
            },
            process.Dispose);
    }

    private static async Task ReleaseAfterExitAsync(Process process)
    {
        try
        {
            await process.WaitForExitAsync();
            if (HasExited(process)) process.Dispose();
        }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            // An indeterminate exit is not permission to act on a different process.
        }
    }

    private static bool IsClosedPipeFailure(Exception exception) =>
        exception is IOException or InvalidOperationException or ObjectDisposedException;

    private static bool HasExited(Process process)
    {
        process.Refresh();
        return process.HasExited;
    }
}

internal sealed class ControlledProcessFixtureOptions
{
    public TimeSpan StartupTimeout { get; init; } = TimeSpan.FromSeconds(5);

    public TimeSpan MaximumLifetime { get; init; } = TimeSpan.FromMinutes(1);

    public bool SuppressReadySignal { get; init; }

    internal Func<Task>? StartupFailureCleanup { get; init; }

    internal Action<PendingFixtureCleanup>? RetainStartupCleanup { get; init; }
}
