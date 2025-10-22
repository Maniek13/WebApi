using Abstractions.DbContexts;
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

namespace Persistence.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AbstractSOFDbContext, StackOverFlowDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddDbContext<StackOverFlowDbContextRO>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddDbContext<AbstractAppDbContext, AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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
