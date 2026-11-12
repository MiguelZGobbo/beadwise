using BeadWise.Core.Diagnostics;

namespace BeadWise.Core.Tests.Diagnostics;

public sealed class DiagnosticCatalogTests
{
    [Fact]
    public void CatalogExposesOnlyBoundedImplementedSlicesAndMarksFixtureActions()
    {
        var entries = DiagnosticCatalog.Slices;
        Assert.Equal(9, entries.Count);
        Assert.Equal(entries.Count, entries.Select(entry => entry.FeatureId).Distinct().Count());
        Assert.True(Assert.Single(entries, entry => entry.FeatureId == "C-PROCESSES-007").ControlledTestOnly);
        Assert.All(entries, entry => Assert.False(string.IsNullOrWhiteSpace(entry.Limitations)));
        Assert.DoesNotContain(entries, entry => entry.FeatureId.StartsWith("C-NETWORK-", StringComparison.Ordinal));
    }
}
