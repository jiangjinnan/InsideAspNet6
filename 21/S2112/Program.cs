using Microsoft.Net.Http.Headers;

var _random = new Random();
var app = WebApplication.Create();
app.UseExceptionHandler(app2 => app2.Run(httpContext => httpContext.Response.WriteAsync("Error occurred!")));
app.MapGet("/", (HttpResponse response) => {
    response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
    {
        MaxAge = TimeSpan.FromHours(1)
    };

    if (_random.Next() % 2 == 0)
    {
        throw new InvalidOperationException("Manually thrown exception...");
    }
    return response.WriteAsync("Succeed...");
});
app.Run();
