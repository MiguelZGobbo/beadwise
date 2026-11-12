using System.Reflection;
using System.Runtime.InteropServices;
using BeadWise.Windows.Processes;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class ReadOnlyNativeSurfaceTests
{
    [Fact]
    public void DiagnosticImportsDoNotExposeConfigurationMutationApis()
    {
        var imports = typeof(WindowsProcessObservationSource).Assembly.GetTypes()
            .Where(type => type.Namespace?.StartsWith("BeadWise.Windows.Diagnostics", StringComparison.Ordinal) == true)
            .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            .Select(method => method.GetCustomAttribute<DllImportAttribute>())
            .Where(import => import is not null).Select(import => import!.EntryPoint ?? "").ToArray();
        Assert.NotEmpty(imports);
        string[] forbidden = ["ChangeDisplaySettings", "SetDisplayConfig", "SetupDiCallClassInstaller",
            "SetupDiSet", "CM_Disable", "CM_Enable", "CM_Reenumerate", "RegSet", "RegDelete", "CreateProcess"];
        Assert.DoesNotContain(imports, entry => forbidden.Any(prefix => entry.StartsWith(prefix, StringComparison.Ordinal)));
    }
}
