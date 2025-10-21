using Newtonsoft.Json;

namespace Domain.Dtos;

public record UserDto
{
    [JsonProperty("account_id")]
    public long AccountId { get; init; }

    [JsonProperty("display_name")]
    public string DispalaName { get; init; }

    [JsonProperty("creation_date")]
    public long CreatedAt { get; init; }
}
