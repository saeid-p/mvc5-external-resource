using System.Web;
using System.Web.Routing;
using MimeTypes;

namespace Presentation.Common.Web.Configurations
{
    public class CommonResourceHttpHandler : IHttpHandler
    {
        private readonly RouteData _routeData;
        public CommonResourceHttpHandler(RouteData routeData)
        {
            _routeData = routeData;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var routeDataValues = _routeData.Values;
            
            // Form request physical path.
            var fileDirectory = routeDataValues["directory"].ToString();
            var fileName = routeDataValues["file"].ToString();
            var fileExtension = routeDataValues["extension"].ToString();
            var nameSpace = typeof(CommonResourceHttpHandler).Assembly.GetName().Name;
            
            // Combine resource address.
            var manifestResourceName = string.Format(
                "{0}.{1}.{2}.{3}", nameSpace, fileDirectory, fileName, fileExtension);

            var stream = typeof(CommonResourceHttpHandler).Assembly.GetManifestResourceStream(manifestResourceName);
            context.Response.Clear();
            context.Response.ContentType = MimeTypeMap.GetMimeType(fileExtension);
            if (stream != null) stream.CopyTo(context.Response.OutputStream);
        }
    }
}
