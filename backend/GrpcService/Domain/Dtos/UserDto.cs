using Newtonsoft.Json;

namespace Domain.Dtos;

public record UserDto
{
    [JsonProperty("user_id")]
    public long UserId { get; init; }
    [JsonProperty("display_name")]
    public string DispalaName { get; init; }

    [JsonProperty("creation_date")]
    public long CreatedAt { get; init; }
}
