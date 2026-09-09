namespace BeadWise.Core.Operations;

public sealed record ExecutionResource
{
    public ExecutionResource(string resourceType, string resourceKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceType);
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceKey);

        ResourceType = resourceType;
        ResourceKey = resourceKey;
    }

    public string ResourceType { get; }

    public string ResourceKey { get; }
}
