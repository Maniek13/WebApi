using Newtonsoft.Json;

namespace Contracts.Dtos.StackOverFlow;

public record QuestionDto(
            [JsonProperty("question_id")]
            long QuestionId,
            string Title,
            [JsonProperty("owner")]
            MemberDto Member,
            string[] Tags,
            string Link,
            [JsonProperty("creation_date")]
            long CreateDateTimeStamp
    );


