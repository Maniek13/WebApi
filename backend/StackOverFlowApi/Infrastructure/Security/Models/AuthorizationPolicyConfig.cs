namespace Infrastructure.Security.Models;

public class AuthorizationPolicyConfig
{
    public string Name { get; set; } = string.Empty;
    public List<string>? RequiredRoles { get; set; }
}
