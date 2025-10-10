using Extensions;
using Microsoft.AspNetCore.Builder;

namespace Configuration.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void SetupWebApi(this WebApplicationBuilder builder)
    {
        builder.SetupModules(
            typeof(Infrastructure.Setup.ModuleSetup).Assembly,
            typeof(Application.Setup.ModuleSetup).Assembly,
            typeof(Persistence.Setup.ModuleSetup).Assembly,
            typeof(Presentation.Setup.ModuleSetup).Assembly);
    }
}
