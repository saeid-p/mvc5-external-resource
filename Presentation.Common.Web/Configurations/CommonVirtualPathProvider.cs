using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;

namespace Presentation.Common.Web.Configurations
{
    public class CommonVirtualPathProvider : VirtualPathProvider
    {
        private readonly VirtualPathProvider _previous;

        public CommonVirtualPathProvider(VirtualPathProvider previous)
        {
            _previous = previous;
        }

        public override bool FileExists(string virtualPath)
        {
            return IsEmbeddedPath(virtualPath) || _previous.FileExists(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return IsEmbeddedPath(virtualPath)
                ? null : _previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            if (!IsEmbeddedPath(virtualDir)) return _previous.GetDirectory(virtualDir);

            var embeddedVirtualDir = virtualDir.Replace(string.Format("~/{0}/", GetEmbeddedPathRoot()), string.Empty);

            var nameSpace = typeof(CommonResourceHttpHandler).Assembly.GetName().Name;
            var manifestResourceName = string.Format("~/{0}.{1}", nameSpace, embeddedVirtualDir);
            return _previous.GetDirectory(manifestResourceName);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            if (!IsEmbeddedPath(virtualDir)) return _previous.DirectoryExists(virtualDir);

            var correctVirtualDir = virtualDir.Replace(string.Format("{0}/", GetEmbeddedPathRoot()), string.Empty);
            return _previous.DirectoryExists(correctVirtualDir);
        }
        public override VirtualFile GetFile(string virtualPath)
        {
            if (!IsEmbeddedPath(virtualPath)) return _previous.GetFile(virtualPath);

            var embeddedVirtualDir = virtualPath
                .Replace(string.Format("~/{0}/", GetEmbeddedPathRoot()), string.Empty)
                .Replace("/", ".");
            var nameSpace = typeof(CommonResourceHttpHandler).Assembly.GetName().Name;
            var manifestResourceName = string.Format("{0}.{1}", nameSpace, embeddedVirtualDir);
            
            var stream = typeof(CommonVirtualPathProvider).Assembly.GetManifestResourceStream(manifestResourceName);
            return new CommonVirtualFile(virtualPath, stream);
        }

        private static bool IsEmbeddedPath(string path)
        {
            return path.Contains(string.Format("~/{0}/", GetEmbeddedPathRoot()));
        }

        private static string GetEmbeddedPathRoot()
        {
            return "Common";
        }
    }
}
