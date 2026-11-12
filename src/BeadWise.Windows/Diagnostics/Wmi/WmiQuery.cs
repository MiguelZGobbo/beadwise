using System.Diagnostics;
using System.Management;

namespace BeadWise.Windows.Diagnostics;

internal interface IWmiQuery
{
    IReadOnlyList<IReadOnlyDictionary<string, object?>> Query(string className, params string[] properties);
}

internal sealed class WmiQuery : IWmiQuery
{
    private static readonly TimeSpan QueryTimeout = TimeSpan.FromSeconds(5);
    private const int MaximumRows = 10000;

    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Query(string className, params string[] properties)
    {
        var options = new System.Management.EnumerationOptions
        {
            ReturnImmediately = true,
            // System.Management clones a rewindable enumerator, allowing both owners to dispose their COM references.
            Rewindable = true,
            BlockSize = 1,
            Timeout = QueryTimeout
        };
        using var searcher = new ManagementObjectSearcher(
            new ManagementScope(@"\\.\root\CIMV2"),
            new ObjectQuery($"SELECT {string.Join(", ", properties)} FROM {className}"),
            options);
        var timer = Stopwatch.StartNew();
        using var collection = searcher.Get();
        using var enumerator = collection.GetEnumerator();
        var rows = new List<IReadOnlyDictionary<string, object?>>();
        while (enumerator.MoveNext())
        {
            using var item = enumerator.Current;
            if (timer.Elapsed > QueryTimeout || rows.Count >= MaximumRows)
            {
                throw new TimeoutException("The WMI inventory query exceeded its limit.");
            }

            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            foreach (var property in properties)
            {
                row.Add(property, item[property]);
            }

            rows.Add(row);
        }

        if (timer.Elapsed > QueryTimeout)
        {
            throw new TimeoutException("The WMI inventory query exceeded its limit.");
        }

        return rows;
    }
}
