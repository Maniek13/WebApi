namespace Shared.Atributes;

public class SaveDbContextAttribute : Attribute
{
    public SaveDbContextAttribute(Type contextType)
    {
        ContextType = contextType;
    }

    public Type ContextType { get; }
}
