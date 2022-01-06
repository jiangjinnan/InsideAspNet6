using Microsoft.Net.Http.Headers;

var app = WebApplication.Create();
app.UseResponseCaching();
app.MapGet("/{foobar?}", Process);
app.Run();

static DateTimeOffset Process(HttpResponse response)
{
    response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(3600)
    };
    return DateTimeOffset.Now;
}
