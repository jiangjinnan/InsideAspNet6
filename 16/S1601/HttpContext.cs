using System.Collections.Specialized;

namespace App
{
    public class HttpContext
    {
        public HttpRequest Request { get; }
        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }

    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;
        public Uri? Url => _feature.Url;
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public HttpRequest(IFeatureCollection features)
            => _feature = features.Get<IHttpRequestFeature>()
            ?? throw new InvalidOperationException("IHttpRequestFeature does not exist.");
    }

    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;

        public NameValueCollection Headers
            => _feature.Headers;
        public Stream Body
            => _feature.Body;
        public int StatusCode
        {
            get => _feature.StatusCode;
            set => _feature.StatusCode = value;
        }

        public HttpResponse(IFeatureCollection features)
            => _feature = features.Get<IHttpResponseFeature>()
            ?? throw new InvalidOperationException("IHttpResponseFeature does not exist.");
    }
}