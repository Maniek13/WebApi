using Abstarction.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Persistence.Repositories;
using Persistence.StartupTasks;

namespace Persistence.Instalation;

public static class Instalation
{
    public static void PersistenceSetup(this WebApplicationBuilder builder)
    {
        DatabaseMigration.EnsureDatabaseExists(builder.Configuration.GetConnectionString("Default")!);
        DatabaseMigration.RunMigrations(builder.Configuration.GetConnectionString("Default")!);

        builder.Services.AddSingleton<ISessionFactory>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            return NHibernateHelper.CreateSessionFactory(configuration);

        });

        builder.Services.AddScoped<ISession>(sp =>
        {
            var sessionFactory = sp.GetRequiredService<ISessionFactory>();
            return sessionFactory.OpenSession();
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();

    }
}
