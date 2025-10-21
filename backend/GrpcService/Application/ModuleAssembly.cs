using System.Reflection;

namespace Application;

public class ModuleAssembly
{
    public static Assembly GetExecutionAssembly => typeof(ModuleAssembly).Assembly;
}
