using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace ServiceTests;

public class TagsRepositoryROTests
{
    readonly Random _random = new Random();
    readonly TagsRepositoryRO _repository;
    readonly List<Tag> _tags = [];

    public TagsRepositoryROTests()
    {
        Environment.SetEnvironmentVariable("TestsVariable", "InMemoryDatabase");

        string dbName = Guid.NewGuid().ToString();


        for (int i = 0; i < 100; ++i)
            _tags.Add(Tag.Create(_random.Next(0, 1000000000).ToString(), _random.Next(0, 1000000000)));

        StackOverFlowDbContext context = new(new DbContextOptionsBuilder<StackOverFlowDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options);

        context.AddRange(_tags);
        context.SaveChanges();

        StackOverFlowDbContextRO contextRO = new(new DbContextOptionsBuilder<StackOverFlowDbContextRO>()
            .UseInMemoryDatabase(dbName)
            .Options);

        _repository = new TagsRepositoryRO(contextRO);
    }

    [Fact]
    public async Task ShouldGetTenTags()
    {
        var tags = await _repository.GetTagsAsync(1, 10, "Id", false, new CancellationToken());


        tags.Items.Should().HaveCount(10);
        tags.PageNumber.Should().Be(1);
        tags.PageSize.Should().Be(10);
        tags.HasNextPage.Should().BeTrue();
        tags.TotalCount.Should().Be(100);
    }

    [Fact]
    public async Task ShouldCheckHaveData()
    {
        var res = await _repository.CheckHaveData(new CancellationToken());
        res.Should().BeTrue();
    }
}
