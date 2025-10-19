using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Mapster;

namespace Application.Mapping.StackOverFlow;

public class QuestionsMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<QuestionDto, Question>()
           .MapWith(el => Question.Create(
                   el.Title,
                   el.Tags,
                   el.Link,
                   el.CreateDateTimeStamp
               ));

    }
}
