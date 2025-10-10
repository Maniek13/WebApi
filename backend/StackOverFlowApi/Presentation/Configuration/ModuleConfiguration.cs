using Abstractions.Configuration;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;

namespace Presentation.Configuration;

public class ModuleConfiguration : IModuleConfiguration
{
    public void SetUp(WebApplicationBuilder builder)
    {

        builder.Services
            .AddFastEndpoints();

        builder.Services.SwaggerDocument(o =>
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
