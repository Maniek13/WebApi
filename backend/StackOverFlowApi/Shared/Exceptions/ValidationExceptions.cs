namespace Shared.Exceptions;

public class ValidationExceptions : Exception
{
    public override string Message { get; }
    public ValidationExceptions(string message)
    {
        Message = message;
    }
}
