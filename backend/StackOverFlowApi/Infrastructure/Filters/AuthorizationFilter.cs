using Hangfire.Dashboard;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Filters;

public class AuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context) => true;
}
