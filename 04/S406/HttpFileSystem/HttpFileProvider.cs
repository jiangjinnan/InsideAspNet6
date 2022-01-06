using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace HttpFileSystem
{
public class HttpFileProvider : IFileProvider, IDisposable
{
    private readonly string _baseAddress;
    private readonly HttpClient _httpClient;

    public HttpFileProvider(string baseAddress)
    {
        _baseAddress = baseAddress.TrimEnd('/');
        _httpClient = new HttpClient();
    }
       
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        string url = $"{_baseAddress}/{subpath.TrimStart('/')}?dir-meta";
        string content = _httpClient.GetStringAsync(url).Result;
        var descriptor = JsonConvert.DeserializeObject<HttpDirectoryContentsDescriptor>(content);
        return descriptor != null ? new HttpDirectoryContents(descriptor, _httpClient): new NotFoundDirectoryContents();
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        string url = $"{_baseAddress}/{subpath.TrimStart('/')}?file-meta";
        string content = _httpClient.GetStringAsync(url).Result;
        var descriptor = JsonConvert.DeserializeObject<HttpFileDescriptor>(content);
        return descriptor?.ToFileInfo(_httpClient)?? new NotFoundFileInfo(subpath);
    }

    public IChangeToken Watch(string filter) => NullChangeToken.Singleton;

    public void Dispose() => _httpClient.Dispose();
}

}
