namespace Domain.Dtos.StackOverFlow;

public record TagDto(
            string Name,
            long Count,
            double? Participation
    );
