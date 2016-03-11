using System.Web.Routing;
using Presentation.Common.Web.Configurations;

namespace Presentation.Common.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.Insert(0,
                new Route("Common/{directory}/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { directory = "Scripts|Content|fonts" }),
                    new CommonResourceRouteHandler()
                ));
        }
    }
}