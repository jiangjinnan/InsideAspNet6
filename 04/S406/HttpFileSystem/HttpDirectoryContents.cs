using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace HttpFileSystem
{
    public class HttpDirectoryContents : IDirectoryContents
    {
        private readonly IEnumerable<IFileInfo> _fileInfos;
        public bool Exists { get; }

        public HttpDirectoryContents(HttpDirectoryContentsDescriptor descriptor, HttpClient httpClient)
        {
            Exists = descriptor.Exists;
            _fileInfos = descriptor.FileDescriptors.Select(file => file.ToFileInfo(httpClient));
        }

        public IEnumerator<IFileInfo> GetEnumerator() => _fileInfos.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _fileInfos.GetEnumerator();
    }

}
