using Abstractions.Interfaces;
using Abstractions.Setup;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace Persistence.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<StackOverFlowDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddDbContext<StackOverFlowDbContextRO>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddScoped<ITagsRepository, TagsRepository>();
        builder.Services.AddScoped<ITagsRepositoryRO, TagsRepositoryRO>();
    }
}
