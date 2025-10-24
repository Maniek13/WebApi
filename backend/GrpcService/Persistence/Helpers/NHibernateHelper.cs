using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Driver;
using Persistence.Mappings;

public class NHibernateHelper
{
    public static ISessionFactory CreateSessionFactory(IConfiguration configuration) =>
        Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(configuration.GetConnectionString("Default"))
                .Driver<MicrosoftDataSqlClientDriver>()
                .ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
            .BuildSessionFactory();
}