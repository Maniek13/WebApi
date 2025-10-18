using Abstractions.Caches;
using Abstractions.Repositories;
using Application.Commands.StackOverFlow;
using Domain.Entities.StackOverFlow;
using FluentAssertions;
using Infrastructure.Services.CacheServices;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.DbContexts.StackOverFlow;
using Persistence.Repositories.StackOverFlow;
using Testcontainers.MsSql;

namespace IntegrationTests.Hendlers;

public class GetTagsQueryHandlerTests : IAsyncLifetime
{
    private readonly Random _random = new Random();
    private readonly IServiceProvider _provider;
    private readonly MsSqlContainer _dbConteiner = new MsSqlBuilder()
        .Build();

    public GetTagsQueryHandlerTests()
    {
        Environment.SetEnvironmentVariable("TestsVariable", "IntegrationTests");

        Task.Run(async () => await _dbConteiner.StartAsync()).Wait();
        var service = new ServiceCollection();

        service.AddDbContext<StackOverFlowDbContext>(o =>
            o.UseSqlServer(_dbConteiner.GetConnectionString()));

        service.AddDbContext<StackOverFlowDbContextRO>(o =>
            o.UseSqlServer(_dbConteiner.GetConnectionString()));

        service.AddScoped<ILoggerFactory, LoggerFactory>();
        service.AddMemoryCache();
        service.AddScoped<ICacheVersionService, CacheVersionService>();
        service.AddScoped<ITagsRepositoryRO, TagsRepositoryRO>();
        service.AddMapster();
        TypeAdapterConfig.GlobalSettings.Scan(Application.ModuleAssembly.GetExecutionAssembly);
        service.AddScoped<GetTagsQueryHandler>();
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetTagsQueryHandler).Assembly));

        _provider = service.BuildServiceProvider();

        using var scope = _provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>();
        db.Database.Migrate();

        List<Tag> tags = [];

        for (int i = 0; i < 100; ++i)
            db.Tags.Add(Tag.Create(_random.Next(0, 10000000).ToString(), _random.Next(0, 10000000)));

        db.SaveChanges();
    }

    [Fact]
    public async Task ShouldGetTenTags()
    {
        using var scope = _provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var query = new GetTagsQuery
        {
            PageSize = 10
        };

        var res = await mediator.Send(query);


        res.PageSize.Should().Be(10);
        res.Items.Should().HaveCount(10);
    }


    public async Task InitializeAsync()
    {
        await _dbConteiner.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbConteiner.DisposeAsync();
    }
}
