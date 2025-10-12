using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.ExternalApies;
using Abstractions.Interfaces;
using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Api;
using Infrastructure.Cache;
using Infrastructure.Services;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.DbContexts;
using Persistence.Repositories;
using Testcontainers.MsSql;

namespace IntegrationTests.Hendlers;

public class RefreshTagsQueryHandlerTests : IAsyncLifetime
{
    private readonly Random _random = new Random();
    private readonly IServiceProvider _provider;
    private readonly MsSqlContainer _dbConteiner = new MsSqlBuilder()
        .Build();

    public RefreshTagsQueryHandlerTests()
    {
        Environment.SetEnvironmentVariable("TestsVariable", "IntegrationTests");

        Task.Run(async () => await _dbConteiner.StartAsync()).Wait();

        var service = new ServiceCollection();

        service.AddDbContext<StackOverFlowDbContext>(o =>
            o.UseSqlServer(_dbConteiner.GetConnectionString()));

        service.AddDbContext<StackOverFlowDbContextRO>(o =>
            o.UseSqlServer(_dbConteiner.GetConnectionString()));

        service.AddScoped<ILoggerFactory, LoggerFactory>();
        service.AddScoped<IStackOverFlowDataService, StackOverFlowDataService>();
        service.AddScoped<IStackOverFlowApiClient, StackOverFlowApiClient>();
        service.AddScoped<ITagsRepository, TagsRepository>();
        service.AddScoped<ITagsRepositoryRO, TagsRepositoryRO>();
        service.AddScoped<ICacheVersionService, CacheVersionService>();
        service.AddHttpClient();
        service.AddMapster();
        TypeAdapterConfig.GlobalSettings.Scan(Application.ModuleAssembly.GetExecutionAssembly);
        service.AddScoped<RefreshTagsQueryHandler>();
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RefreshTagsQueryHandler).Assembly));

        _provider = service.BuildServiceProvider();

        using var scope = _provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>();
        db.Database.Migrate();

        List<Tag> tags = [];

        for (int i = 0; i < 100; ++i)
            db.tags.Add(Tag.Create(_random.Next(0, 10000000).ToString(), _random.Next(0, 10000000)));

        db.SaveChanges();

    }

    [Fact]
    public async Task ShouldRefreshTags()
    {
        using var scope = _provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var query = new RefreshTagsQuery
        {
        };

        Func<Task> refresh = () => mediator.Send(query);

        await refresh
            .Should()
            .NotThrowAsync();
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
