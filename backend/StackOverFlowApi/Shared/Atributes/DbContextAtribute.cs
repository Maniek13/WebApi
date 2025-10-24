namespace Shared.Atributes;

public class DbContextAtribute : Attribute
{
    public DbContextAtribute(Type contextType)
    {
        ContextType = contextType;
    }

    public Type ContextType { get; }
}
