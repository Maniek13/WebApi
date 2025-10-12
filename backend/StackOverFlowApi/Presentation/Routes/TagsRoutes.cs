using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Routes;

internal class TagsRoutes
{
    public const string BaseRoute = "Tags";
    public const string Get = BaseRoute;
    public const string Refresh = $"{BaseRoute}/RefreshData";
}
