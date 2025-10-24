using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Mapster;
using System.Reflection.Metadata.Ecma335;

namespace Application.Mapping.StackOverFlow;

public class QuestionsMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<QuestionDto, Question>()
           .MapWith(el => Question.Create(
                   el.QuestionId,
                   el.Member.UserId,
                   el.Title,
                   el.Tags,
                   el.Link,
                   el.CreateDateTimeStamp
               ));
        cfg.NewConfig<(QuestionDto dto, long? userId), Question>()
           .MapWith(el => Question.Create(
                   el.dto.QuestionId,
                   el.userId,
                   el.dto.Title,
                   el.dto.Tags,
                   el.dto.Link,
                   el.dto.CreateDateTimeStamp
               ));

        cfg.NewConfig<(QuestionDto dto, long? userId), QuestionDto>()
           .MapWith(el => new QuestionDto(
                   el.dto.QuestionId,
                   el.dto.Title,
                   new MemberDto(el.userId),
                   el.dto.Tags,
                   el.dto.Link,
                   el.dto.CreateDateTimeStamp
               ));

    }
}
