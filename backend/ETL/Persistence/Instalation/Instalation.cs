using Abstractions.DbContexts;
using Abstractions.Interfaces;
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
        builder.Services.AddDbContext<StackOverFlowDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);
        var serviceProvider = builder.Services.BuildServiceProvider();

        builder.Services.AddScoped<AbstractSOFDbContext>(sp => sp.GetRequiredService<StackOverFlowDbContext>());
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        if (builder.Environment.EnvironmentName == "Development")
        {
            using var scope = serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>().Database.Migrate();
        }
    }
}
