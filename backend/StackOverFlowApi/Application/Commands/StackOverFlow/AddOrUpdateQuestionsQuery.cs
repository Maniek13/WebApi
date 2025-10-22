using Contracts.Dtos.StackOverFlow;
using MediatR;

namespace Application.Commands.StackOverFlow
{
    public class AddOrUpdateQuestionsQuery : IRequest
    {
        public FechQuestionDto QuestionsWithNotExistedUsers { get; set; }
    }

}
