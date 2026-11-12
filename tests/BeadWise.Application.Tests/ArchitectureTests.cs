using System.Reflection;
using System.Runtime.InteropServices;
using BeadWise.Application.Diagnostics;
using BeadWise.Core.Sessions;

namespace BeadWise.Application.Tests;

public sealed class ArchitectureTests
{
    [Fact]
    public void CoreAndApplicationDoNotReferenceWindowsAdaptersOrUi()
    {
        foreach (var assembly in new[] { typeof(Session).Assembly, typeof(GetDiagnosticInventoryUseCase).Assembly })
        {
            Assert.DoesNotContain(assembly.GetReferencedAssemblies(), reference =>
                reference.Name is "BeadWise.Windows" or "BeadWise.App" or "System.Management" or "Microsoft.WindowsAppSDK");
            var methods = assembly.GetTypes().SelectMany(type => type.GetMethods(
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
            Assert.DoesNotContain(methods, method => method.GetCustomAttribute<DllImportAttribute>() is not null);
        }
    }
}
