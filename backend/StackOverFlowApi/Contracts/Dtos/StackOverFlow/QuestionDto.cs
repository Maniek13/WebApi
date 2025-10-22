using Newtonsoft.Json;

namespace Contracts.Dtos.StackOverFlow;

public record QuestionDto(
            string Title,
            [JsonProperty("owner")]
            MemberDto Member,
            string[] Tags,
            string Link,
            [JsonProperty("creation_date")]
            long CreateDateTimeStamp
    );


