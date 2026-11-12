using System.Diagnostics;
using BeadWise.Core.Processes;

namespace BeadWise.Windows.Processes;

internal sealed class ControlledProcessTarget
{
    private const string FixtureExecutableName = "BeadWise.ControlledProcessFixture";
    private const string FixtureModeArgument = "--beadwise-controlled-fixture";
    private const string FixtureKeyArgument = "--fixture-key";

    public ControlledProcessTarget(
        ProcessIdentity expectedIdentity,
        Func<bool> isOwnedLaunchContext,
        Func<bool> isRunning,
        Func<ProcessIdentity> readIdentity,
        Action kill)
    {
        ArgumentNullException.ThrowIfNull(isOwnedLaunchContext);
        ArgumentNullException.ThrowIfNull(isRunning);
        ArgumentNullException.ThrowIfNull(readIdentity);
        ArgumentNullException.ThrowIfNull(kill);

        ExpectedIdentity = expectedIdentity;
        IsOwnedLaunchContext = isOwnedLaunchContext;
        IsRunning = isRunning;
        ReadIdentity = readIdentity;
        Kill = kill;
    }

    public ProcessIdentity ExpectedIdentity { get; }

    public Func<bool> IsOwnedLaunchContext { get; }

    public Func<bool> IsRunning { get; }

    public Func<ProcessIdentity> ReadIdentity { get; }

    public Action Kill { get; }

    public static ControlledProcessTarget FromOwnedFixture(
        Process process,
        Guid fixtureKey,
        ProcessIdentity expectedIdentity)
    {
        ArgumentNullException.ThrowIfNull(process);

        return new ControlledProcessTarget(
            expectedIdentity,
            () => HasOwnedLaunchContext(process, fixtureKey),
            () => ReadRunningState(process),
            () => ReadProcessIdentity(process),
            process.Kill);
    }

    private static bool HasOwnedLaunchContext(Process process, Guid fixtureKey)
    {
        var startInfo = process.StartInfo;
        var arguments = startInfo.ArgumentList;

        return !startInfo.UseShellExecute &&
            startInfo.RedirectStandardInput &&
            startInfo.RedirectStandardOutput &&
            startInfo.RedirectStandardError &&
            string.Equals(
                Path.GetFileNameWithoutExtension(startInfo.FileName),
                FixtureExecutableName,
                StringComparison.OrdinalIgnoreCase) &&
            arguments.Count >= 3 &&
            arguments[0] == FixtureModeArgument &&
            arguments[1] == FixtureKeyArgument &&
            arguments[2] == fixtureKey.ToString("D");
    }

    private static bool ReadRunningState(Process process)
    {
        process.Refresh();
        return !process.HasExited;
    }

    private static ProcessIdentity ReadProcessIdentity(Process process)
    {
        process.Refresh();
        return new ProcessIdentity(
            process.Id,
            new DateTimeOffset(process.StartTime.ToUniversalTime()));
    }
}
