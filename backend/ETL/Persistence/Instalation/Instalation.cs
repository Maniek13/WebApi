using Abstractions.DbContexts;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts.StackOverFlow;
using Persistence.Repositories.StackOverFlow;

namespace Persistence.Instalation;

public static class Instalation
{
    public static void PersistenceSetup(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AbstractSOFDbContext, StackOverFlowDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        var serviceProvider = builder.Services.BuildServiceProvider();

        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

        if (builder.Environment.EnvironmentName == "Development")
        {
            using var scope = serviceProvider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AbstractSOFDbContext>();
            ctx.Database.EnsureCreated();
        }
    }
}
