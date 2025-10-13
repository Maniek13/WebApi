using Domain.Entities.StackOverFlow;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts.StackOverFlow;
using Persistence.Repositories.StackOverFlow;

namespace RepositoryTests;

public class TagsRepositoryTests
{
    public TagsRepositoryTests()
    {
        Environment.SetEnvironmentVariable("TestsVariable", "InMemoryDatabase");
    }

    [Fact]
    public async Task ShouldSetTagsWhenGetDate()
    {
        Random random = new Random();

        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<StackOverFlowDbContext>()
            .UseSqlite(connection)
            .EnableSensitiveDataLogging()
            .Options;

        using StackOverFlowDbContext context = new(options);
        context.Database.EnsureCreated();

        var repository = new TagsRepository(context);

        List<Tag> tags = [
                Tag.Create(random.Next(0, 1000000000).ToString(), random.Next(0, 1000000000)),
                Tag.Create(random.Next(0, 1000000000).ToString(), random.Next(0, 1000000000))
            ];

        await repository.SetTagsAsync(tags, CancellationToken.None);

        var dbTags = context.tags;

        dbTags.Should().HaveCount(tags.Count);

        for (int i = 0; i < tags.Count; ++i)
            dbTags.Where(el => el.Id == tags[i].Id).First().Should().Be(tags[i]);
    }
}