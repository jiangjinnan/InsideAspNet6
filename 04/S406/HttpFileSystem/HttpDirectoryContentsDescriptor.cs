using Microsoft.Extensions.FileProviders;

namespace HttpFileSystem
{
    public class HttpDirectoryContentsDescriptor
    {
        public bool Exists { get; set; }
        public IEnumerable<HttpFileDescriptor> FileDescriptors { get; set; }

        public HttpDirectoryContentsDescriptor()
        {
            FileDescriptors = Array.Empty<HttpFileDescriptor>();
        }

        public HttpDirectoryContentsDescriptor(IDirectoryContents directoryContents, Func<string, string> physicalPathResolver)
        {
            Exists = directoryContents.Exists;
            FileDescriptors = directoryContents.Select(_ => new HttpFileDescriptor(_, physicalPathResolver));
        }
    }

}
