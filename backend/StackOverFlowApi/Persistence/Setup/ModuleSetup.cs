using Abstractions.DbContexts;
using Abstractions.Interfaces;
using Abstractions.Persistence;
using Abstractions.Repositories;
using Abstractions.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts.App;
using Persistence.DbContexts.StackOverFlow;
using Persistence.Repositories.StackOverFlow;
using Persistence.StartupTasks;
using Persistence.UnitsOfWorks;

namespace Persistence.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<StackOverFlowDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);

        builder.Services.AddDbContext<StackOverFlowDbContextRO>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);


        builder.Services.AddScoped<AbstractSOFDbContext>(s => s.GetRequiredService<StackOverFlowDbContext>());
        builder.Services.AddScoped<AbstractAppDbContext>(s => s.GetRequiredService<AppDbContext>());

        builder.Services.AddScoped<ISofUnitOfWork<AbstractSOFDbContext>, SofUnitOfWork>();
        builder.Services.AddScoped<IApiUnitOfWork<AbstractAppDbContext>, AppUnitOfWork>();

        builder.Services.AddScoped<ITagsRepository, TagsRepository>();
        builder.Services.AddScoped<ITagsRepositoryRO, TagsRepositoryRO>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IQuestionsRepositoryRO, QuestionsRepositoryRO>();
        builder.Services.AddScoped<IUsersRepositoryRO, UsersRepositoryRO>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        var serviceProvider = builder.Services.BuildServiceProvider();

        if (builder.Environment.EnvironmentName == "Development")
            MigrationInitializer.ApplyMigrations(serviceProvider);

    }
}
