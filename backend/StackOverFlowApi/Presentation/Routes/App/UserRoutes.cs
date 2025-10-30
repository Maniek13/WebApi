namespace Presentation.Routes.App;

public class UserRoutes
{
    public const string BaseRoute = $"{App.BaseRoute.Base}/Users";
    public const string Login = $"{BaseRoute}/Login";
    public const string Register = $"{BaseRoute}/Register";
    public const string Refresh = $"{BaseRoute}/Refresh";
    public const string SetAddress = $"{BaseRoute}/SetAddress";
    public const string AddMessage = $"{BaseRoute}/AddMessage";
}
