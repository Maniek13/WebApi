using System.Reflection;

namespace Persistence;

internal class ModuleAssembly
{
    internal static Assembly GetExecutionAssembly => typeof(ModuleAssembly).Assembly;
}
