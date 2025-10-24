using Newtonsoft.Json;

namespace Contracts.Dtos.StackOverFlow;

public record MemberDto
(
    [JsonProperty("user_id")]
    long? UserId
);
