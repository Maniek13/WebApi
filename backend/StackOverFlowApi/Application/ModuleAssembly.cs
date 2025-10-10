using System.Reflection;

namespace Application;

internal class ModuleAssembly
{
    internal static Assembly GetExecutionAssembly => typeof(ModuleAssembly).Assembly;
}
