namespace BeadWise.ControlledProcessFixture;

public static class ControlledProcessFixtureHost
{
    private const string FixtureModeArgument = "--beadwise-controlled-fixture";
    private const string FixtureKeyArgument = "--fixture-key";
    private const string MaximumLifetimeArgument = "--max-lifetime-ms";
    private const string SuppressReadyArgument = "--suppress-ready";
    private const string ReadyPrefix = "BEADWISE_FIXTURE_READY";
    private const string ShutdownPrefix = "BEADWISE_FIXTURE_SHUTDOWN";
    public static async Task<int> Main(string[] args)
    {
        if (!TryReadConfiguration(
                args,
                out var fixtureKey,
                out var maximumLifetime,
                out var suppressReadySignal))
        {
            return 2;
        }

        if (!suppressReadySignal)
        {
            Console.WriteLine($"{ReadyPrefix}|{fixtureKey:D}|{Environment.ProcessId}");
            await Console.Out.FlushAsync();
        }

        var lifetimeExpired = Task.Delay(maximumLifetime);
        while (true)
        {
            var readCommand = Task.Run(Console.ReadLine);
            if (await Task.WhenAny(readCommand, lifetimeExpired) == lifetimeExpired)
            {
                return 0;
            }

            var command = await readCommand;
            if (command is null || command == $"{ShutdownPrefix}|{fixtureKey:D}")
            {
                return 0;
            }
        }
    }

    private static bool TryReadConfiguration(
        string[] args,
        out Guid fixtureKey,
        out TimeSpan maximumLifetime,
        out bool suppressReadySignal)
    {
        fixtureKey = default;
        maximumLifetime = default;
        suppressReadySignal = false;

        if (args.Length is not (5 or 6))
        {
            return false;
        }

        if (args[0] != FixtureModeArgument
            || args[1] != FixtureKeyArgument
            || !Guid.TryParseExact(args[2], "D", out fixtureKey)
            || args[3] != MaximumLifetimeArgument
            || !int.TryParse(args[4], out var maximumLifetimeMilliseconds)
            || maximumLifetimeMilliseconds <= 0
            || (args.Length == 6 && args[5] != SuppressReadyArgument))
        {
            return false;
        }

        maximumLifetime = TimeSpan.FromMilliseconds(maximumLifetimeMilliseconds);
        suppressReadySignal = args.Length == 6;
        return true;
    }
}
