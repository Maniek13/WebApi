using Abstractions.Jobs;

namespace Infrastructure.Jobs;

public class ConfigureJobs
{
    public static void SetRecurngJobs()
    {
        var jobs = ModuleAssembly.GetExecutionAssembly
            .GetTypes()
            .Where(el => typeof(IJobSetter).IsAssignableFrom(el) && !el.IsAbstract && !el.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IJobSetter>();

        foreach (var job in jobs)
        {
            job.AddRecuringJob();
        }
    }
}
