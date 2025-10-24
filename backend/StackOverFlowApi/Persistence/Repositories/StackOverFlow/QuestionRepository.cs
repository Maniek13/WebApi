using Abstractions.Repositories;
using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.Repositories.StackOverFlow;

public class QuestionRepository : IQuestionRepository
{
    private readonly StackOverFlowDbContext _dbContext;

    public QuestionRepository(StackOverFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrUpdateQuestionsAsync(List<Question> questions, CancellationToken ct)
    {
        for (int i = 0; i < questions.Count; ++i)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(el => el.QuestionId == questions[i].QuestionId);

            if (question == null)
                await _dbContext.Questions.AddAsync(questions[i]!, ct);
            else
            {
                question.Update(questions[i].Tags, questions[i].Link, questions[i].Title);
            }
        }
    }
}
