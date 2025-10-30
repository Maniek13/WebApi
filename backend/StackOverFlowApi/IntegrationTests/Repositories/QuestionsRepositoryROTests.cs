using Abstractions.DbContexts;
using Abstractions.Interfaces;
using Domain.Entities.StackOverFlow;
using Domain.Entities.StackOverFlow.ValueObjects;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts.StackOverFlow;
using Persistence.Repositories.StackOverFlow;

namespace IntegrationTests.Repositories;

public class QuestionsRepositoryROTests
{

    [Fact]
    public async Task ShouldGetAllWehnHaveData()
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

        List<Question> questions = [
                Question.Create((QuestionNumber)1, UserNumber.CreateEmpty(), random.Next(0, 1000000000).ToString(), [], random.Next(0, 1000000000).ToString(), random.Next(0, 1000000000)),
                Question.Create((QuestionNumber)1, (UserNumber)1,random.Next(0, 1000000000).ToString(), [], random.Next(0, 1000000000).ToString(), random.Next(0, 1000000000)),
            ];

        await context.Questions.AddRangeAsync(questions, CancellationToken.None);

        await context.SaveChangesAsync();

        var dbQuestions = context.Questions;

        dbQuestions.Should().HaveCount(questions.Count);

        for (int i = 0; i < questions.Count; ++i)
            dbQuestions.Where(el => el.Id == questions[i].Id).First().Should().Be(questions[i]);
    }
}