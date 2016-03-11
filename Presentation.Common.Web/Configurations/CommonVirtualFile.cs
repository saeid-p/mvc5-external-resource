using System.IO;
using System.Web.Hosting;

namespace Presentation.Common.Web.Configurations
{
    public class CommonVirtualFile : VirtualFile
    {
        private readonly Stream _stream;

        public CommonVirtualFile(string virtualPath, Stream stream)
            : base(virtualPath)
        {
            _stream = stream;
        }

        public override Stream Open()
        {
            return _stream;
        }
    } 
}