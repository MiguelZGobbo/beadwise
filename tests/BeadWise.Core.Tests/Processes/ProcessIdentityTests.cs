using BeadWise.Core.Processes;

namespace BeadWise.Core.Tests.Processes;

public sealed class ProcessIdentityTests
{
    private static readonly DateTimeOffset CreationTime = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void SameProcessIdAndCreationTimeAreEqual()
    {
        var first = new ProcessIdentity(42, CreationTime);
        var second = new ProcessIdentity(42, CreationTime);

        Assert.Equal(first, second);
    }

    [Fact]
    public void SameProcessIdWithDifferentCreationTimeIsNotEqual()
    {
        var first = new ProcessIdentity(42, CreationTime);
        var second = new ProcessIdentity(42, CreationTime.AddSeconds(1));

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void DifferentProcessIdIsNotEqual()
    {
        var first = new ProcessIdentity(42, CreationTime);
        var second = new ProcessIdentity(43, CreationTime);

        Assert.NotEqual(first, second);
    }
}
