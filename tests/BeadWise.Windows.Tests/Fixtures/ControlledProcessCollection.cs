namespace BeadWise.Windows.Tests.Fixtures;

[CollectionDefinition(Name, DisableParallelization = true)]
public sealed class ControlledProcessCollection
{
    public const string Name = "Controlled process fixture";
}
