using Microsoft.AspNetCore.ConcurrencyLimiter;
using Microsoft.AspNetCore.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Services
   .Configure<ConcurrencyLimiterOptions>(options => options.OnRejected = RejectAsync)
   .AddStackPolicy(options =>
    {
        options.MaxConcurrentRequests = 2;
        options.RequestQueueLimit = 2;
    });
var app = builder.Build();
app
    .UseConcurrencyLimiter()
    .Run(httpContext => Task.Delay(1000).ContinueWith(_ => httpContext.Response.StatusCode = 200));
app.Run();

static Task RejectAsync(HttpContext httpContext)
{
    var request = httpContext.Request;
    if (!request.Query.ContainsKey("reject"))
    {
        var response = httpContext.Response;
        response.StatusCode = 307;
        var queryString = request.QueryString.Add("reject", "true");
        var newUrl = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path, queryString);
        response.Headers.Location = newUrl;
    }
    return Task.CompletedTask;
}

Results