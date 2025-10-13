using System.Reflection;

namespace Infrastructure;

public class ModuleAssembly
{
    public static Assembly GetExecutionAssembly => typeof(ModuleAssembly).Assembly;
}
