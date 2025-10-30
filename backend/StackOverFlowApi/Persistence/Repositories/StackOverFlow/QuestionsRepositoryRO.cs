using Abstractions.Repositories.SOF;
using Domain.Entities.StackOverFlow;
using Persistence.Common;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.Repositories.StackOverFlow;

public class QuestionsRepositoryRO : RepositoryROBase<Question, StackOverFlowDbContextRO>, IQuestionsRepositoryRO
{
    public QuestionsRepositoryRO(StackOverFlowDbContextRO dbContexts) : base(dbContexts)
    {
    }
}
