using System.Web;
using System.Web.Routing;

namespace Presentation.Common.Web.Configurations
{
    public class CommonResourceRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new CommonResourceHttpHandler(requestContext.RouteData);
        }
    }
}