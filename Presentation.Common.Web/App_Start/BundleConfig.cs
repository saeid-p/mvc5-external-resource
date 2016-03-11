using System.Web.Hosting;
using System.Web.Optimization;
using Presentation.Common.Web.Configurations;

namespace Presentation.Common.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.VirtualPathProvider = new CommonVirtualPathProvider(HostingEnvironment.VirtualPathProvider);
            BundleTable.Bundles.Add(new ScriptBundle("~/Common/jQuery")
                .Include("~/Common/Scripts/jquery-2.2.1.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Common/Bootstrap/Script")
                .Include("~/Common/Scripts/bootstrap.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/Common/Bootstrap/Style")
                .Include("~/Common/Content/bootstrap.css"));
        }
    }
}