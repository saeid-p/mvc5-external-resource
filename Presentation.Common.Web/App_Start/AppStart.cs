using System.Web.Optimization;
using System.Web.Routing;
using Presentation.Common.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AppStart), "PostStart")]
namespace Presentation.Common.Web
{
    public static class AppStart
    {
        public static void PostStart()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}