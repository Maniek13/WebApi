using Abstractions.Setup;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {

        builder.Services
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "StackOverFlow Api";
                    s.Version = "v1";
                    s.DocumentName = "Data";
                };
            });
    }
}
