using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

var app = WebApplication.Create();
app.UseResponseCaching();
app.MapGet("/{foobar?}", Process);
app.Run();

static DateTimeOffset Process(HttpResponse response,
    [FromHeader(Name = "X-UTC")] string? utcHeader,
    [FromQuery(Name = "utc")] string? utcQuery)
{
    response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(3600)
    };

    return Parse(utcHeader) ?? Parse(utcQuery) ?? false
        ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

    static bool? Parse(string? value)
    => value == null
    ? null
    : string.Compare(value, "1", true) == 0 || string.Compare(value, "true", true) == 0;
}