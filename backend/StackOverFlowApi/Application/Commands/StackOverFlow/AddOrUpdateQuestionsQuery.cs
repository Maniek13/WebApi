using Abstractions.DbContexts;
using Contracts.Evetnts;
using MediatR;

namespace Application.Commands.StackOverFlow
{
    [Shared.Atributes.SaveDbContextAttribute(typeof(AbstractSOFDbContext))]
    public class AddOrUpdateQuestionsQuery : IRequest
    {
        public QuestionEvent QuestionsWithNotExistedUsers { get; set; }
    }

}
