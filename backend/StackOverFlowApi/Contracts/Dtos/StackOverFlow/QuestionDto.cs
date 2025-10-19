namespace Contracts.Dtos.StackOverFlow;

public record QuestionDto(
            string Title,
            string[] Tags,
            string Link,
            long CreateDateTimeStamp
    );
