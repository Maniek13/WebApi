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

    public TagsRepositoryROTests()
    {
        Environment.SetEnvironmentVariable("TestsVariable", "InMemoryDatabase");

        string dbName = Guid.NewGuid().ToString();
   
        StackOverFlowDbContext context = new(new DbContextOptionsBuilder<StackOverFlowDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options);

        for (int i = 0; i < 100; ++i)
            context.Add(Tag.Create(_random.Next(0, 1000000000).ToString(), _random.Next(0, 1000000000)));

        context.SaveChanges();

        StackOverFlowDbContextRO contextRO = new(new DbContextOptionsBuilder<StackOverFlowDbContextRO>()
            .UseInMemoryDatabase(dbName)
            .Options);

        _repository = new TagsRepositoryRO(contextRO);
    }

    [Fact]
    public async Task ShouldGetTenTags()
    {
        var tags = await _repository.GetTagsAsync(1, 10, "Id", false, CancellationToken.None);


        tags.Items.Should().HaveCount(10);
        tags.PageNumber.Should().Be(1);
        tags.PageSize.Should().Be(10);
        tags.HasNextPage.Should().BeTrue();
        tags.TotalCount.Should().Be(100);
    }

    [Fact]
    public async Task ShouldCheckHaveData()
    {
        var res = await _repository.CheckHaveData(CancellationToken.None);
        res.Should().BeTrue();
    }
}
