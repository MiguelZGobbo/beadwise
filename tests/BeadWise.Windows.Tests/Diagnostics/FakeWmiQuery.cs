using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

internal sealed class FakeWmiQuery : IWmiQuery
{
    private readonly Dictionary<string, IReadOnlyList<IReadOnlyDictionary<string, object?>>> _rows = [];
    private readonly Dictionary<string, Exception> _failures = [];

    public void Set(string className, params IReadOnlyDictionary<string, object?>[] rows) =>
        _rows[className] = rows;

    public void Fail(string className, Exception exception) => _failures[className] = exception;

    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Query(string className, params string[] properties)
    {
        if (_failures.TryGetValue(className, out var failure))
        {
            throw failure;
        }

        return _rows.GetValueOrDefault(className) ?? [];
    }

    public static IReadOnlyDictionary<string, object?> Row(params (string Name, object? Value)[] fields) =>
        fields.ToDictionary(field => field.Name, field => field.Value);
}
