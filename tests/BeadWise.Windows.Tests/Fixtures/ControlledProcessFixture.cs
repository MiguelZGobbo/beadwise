using System.Diagnostics;
using BeadWise.ControlledProcessFixture;
using BeadWise.Core.Processes;

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
    private bool _disposed;

    private ControlledProcessFixture(
        Process process,
        Guid fixtureKey,
        ProcessIdentity identity)
    {
        _process = process;
        FixtureKey = fixtureKey;
        Identity = identity;
    }

    public Guid FixtureKey { get; }

    public int ProcessId => Identity.ProcessId;

    public ProcessIdentity Identity { get; }

    public bool IsRunning => !_disposed && !_process.HasExited;

    public static Task<ControlledProcessFixture> StartAsync() =>
        StartAsync(new ControlledProcessFixtureOptions());

    public static Task<ControlledProcessFixture> StartAsync(
        ControlledProcessFixtureOptions options)
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

        return StartCoreAsync(options, (int)maximumLifetimeMilliseconds);
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
        int maximumLifetimeMilliseconds)
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
            var readyLine = await process.StandardOutput.ReadLineAsync()
                .WaitAsync(options.StartupTimeout);
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
            }
            catch (Exception cleanupFailure)
            {
                startupFailure.Data["BeadWise.ControlledProcessFixture.CleanupFailure"] =
                    cleanupFailure;
            }
            finally
            {
                process.Dispose();
            }

            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        try
        {
            await StopProcessAsync(_process, FixtureKey);
        }
        finally
        {
            _process.Dispose();
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
}
