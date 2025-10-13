using Microsoft.Extensions.Configuration;

namespace Shared.Helpers;

public class ConfigurationHelper
{
    public static IConfiguration GetConfigurationBuilder(string appseting = "appsettings.json") => new ConfigurationBuilder() 
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(appseting, optional: false, reloadOnChange: true)
        .Build();
    
}
