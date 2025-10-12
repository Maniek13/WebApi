using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Persistence.DbContexts;
using Testcontainers.MsSql;

namespace EndToEndTests;

public class WebApiWebAplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MsSqlContainer _dbConteiner;
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.Sources.Clear();

                configBuilder.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
            });

            return base.CreateHost(builder);
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("TestsVariable", "WebApplicationFactory");

        builder.ConfigureServices(services => {
            services.RemoveAll(typeof(StackOverFlowDbContext));
            services.RemoveAll(typeof(StackOverFlowDbContextRO));

            services.AddDbContext<StackOverFlowDbContext>(o => o.UseSqlServer(_dbConteiner.GetConnectionString()));
            services.AddDbContext<StackOverFlowDbContextRO>(o => o.UseSqlServer(_dbConteiner.GetConnectionString()));


            var serviceProvider = services.BuildServiceProvider();



            using var scoped = serviceProvider.CreateScope();

            var db = scoped.ServiceProvider.GetRequiredService<StackOverFlowDbContext>();
            db.Database.Migrate();
        });
    }

    public async Task InitializeAsync()
    {
        _dbConteiner = new MsSqlBuilder()
          .WithImage("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04")
          .Build();


        await _dbConteiner.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbConteiner.DisposeAsync();
    }
}
