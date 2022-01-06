using HttpMachine;
using Microsoft.AspNetCore.Http.Features;

namespace App
{
public class HttpParserHandler : IHttpParserHandler
{
    public HttpRequestFeature Request { get; } = new HttpRequestFeature();

    private string? headerName = null;
    public void OnBody(HttpParser parser, ArraySegment<byte> data)=> Request.Body = new MemoryStream(data.Array!, data.Offset, data.Count);
    public void OnFragment(HttpParser parser, string fragment) { }
    public void OnHeaderName(HttpParser parser, string name)=> headerName = name;
    public void OnHeadersEnd(HttpParser parser) { }
    public void OnHeaderValue(HttpParser parser, string value)=> Request.Headers[headerName!] = value;
    public void OnMessageBegin(HttpParser parser) { }
    public void OnMessageEnd(HttpParser parser) { }
    public void OnMethod(HttpParser parser, string method)=> Request.Method = method;
    public void OnQueryString(HttpParser parser, string queryString)=> Request.QueryString = queryString;
    public void OnRequestUri(HttpParser parser, string requestUri)=> Request.Path = requestUri;
}
}
