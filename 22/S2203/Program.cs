using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Net.Http.Headers;

var app = WebApplication.Create();
app.UseResponseCaching();
app.MapGet("/{foobar?}", Process);
app.Run();

static DateTimeOffset Process(HttpContext httpContext,
    [FromHeader(Name = "X-UTC")] string? utcHeader,
    [FromQuery(Name = "utc")] string? utcQuery)
{
    var response = httpContext.Response;
    response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(3600)
    };

    var feature = httpContext.Features.Get<IResponseCachingFeature>()!;
    feature.VaryByQueryKeys = new string[] { "utc" };
    response.Headers.Vary = "X-UTC";

    return Parse(utcHeader) ?? Parse(utcQuery) ?? false
        ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

    static bool? Parse(string? value)
    => value == null
    ? null
    : string.Compare(value, "1", true) == 0 || string.Compare(value, "true", true) == 0;
}
