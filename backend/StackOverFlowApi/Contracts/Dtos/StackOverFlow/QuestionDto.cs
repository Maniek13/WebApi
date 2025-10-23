using Newtonsoft.Json;

namespace Contracts.Dtos.StackOverFlow;

public record QuestionDto(
            long QuestionId,
            string Title,
            MemberDto Member,
            string[] Tags,
            string Link,
            long CreateDateTimeStamp
    );


