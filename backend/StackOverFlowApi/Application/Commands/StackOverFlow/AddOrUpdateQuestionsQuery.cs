using Contracts.Dtos.StackOverFlow;
using Contracts.Evetnts;
using MediatR;

namespace Application.Commands.StackOverFlow
{
    public class AddOrUpdateQuestionsQuery : IRequest
    {
        public QuestionEvent QuestionsWithNotExistedUsers { get; set; }
    }

}
