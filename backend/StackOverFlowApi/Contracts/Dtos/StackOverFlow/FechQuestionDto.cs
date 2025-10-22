namespace Contracts.Dtos.StackOverFlow;

public record FechQuestionDto
(
    UserDto[] Users,
    QuestionDto[] Questions
);
