using Abstractions.DbContexts;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Persistence.DbContexts.App;
using Persistence.DbContexts.StackOverFlow;
using Testcontainers.MsSql;

namespace EndToEndTests.ApplicationFactory;

public class WebApiWebAplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MsSqlContainer _dbConteiner;
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.Sources.Clear();

            configBuilder.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
        });

        return base.CreateHost(builder);
    }
    

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => {
            services.RemoveAll(typeof(StackOverFlowDbContext));
            services.RemoveAll(typeof(StackOverFlowDbContextRO));
            services.RemoveAll(typeof(AbstractAppDbContext));


            services.AddDbContext<StackOverFlowDbContext>(o => o.UseSqlServer(_dbConteiner.GetConnectionString()));
            services.AddDbContext<StackOverFlowDbContextRO>(o => o.UseSqlServer(_dbConteiner.GetConnectionString()));

            services.AddDbContext<AbstractAppDbContext, AppDbContext>(o =>
                o.UseSqlServer(_dbConteiner.GetConnectionString()));


            services.AddSignalR();

            var serviceProvider = services.BuildServiceProvider();
            ;
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>();
            db.Database.Migrate();

            var appDbContext = scope.ServiceProvider.GetRequiredService<AbstractAppDbContext>();
            appDbContext.Database.Migrate();

            services.AddHangfire(c => c.UseMemoryStorage()); 
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
