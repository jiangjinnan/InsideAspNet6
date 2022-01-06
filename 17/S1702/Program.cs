using App;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var app = builder.Build();
var listener = app.Services.GetRequiredService<DiagnosticListener>();
listener.SubscribeWithAdapter(new DiagnosticCollector());
app.Run(HandleAsync);
app.Run();

static Task HandleAsync(HttpContext httpContext)
{
    var listener = httpContext.RequestServices.GetRequiredService<DiagnosticListener>();
    if (httpContext.Request.Path == new PathString("/error"))
    {
        throw new InvalidOperationException("Manually throw exception.");
    }
    return Task.CompletedTask;
}
