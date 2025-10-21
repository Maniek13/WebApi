namespace Contracts.Responses;

public record TagResponse(
            string Name,
            long Count,
            double? Participation
    );
