using Abstractions.Startup;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Presentation.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfiguration configuration)
    {
        application.UseFastEndpoints();

        if (application.Environment.IsDevelopment())
        {
            application.UseSwaggerGen();
            application.UseSwagger();
            application.UseSwaggerUI();
        }
    }
}
