using System.Diagnostics.Tracing;

var listener = new DiagnosticCollector();
listener.EventSourceCreated += (sender, args) =>
{
    if (args.EventSource?.Name == "Microsoft.AspNetCore.Hosting")
    {
        listener.EnableEvents(args.EventSource, EventLevel.LogAlways);
    }
};
listener.EventWritten += (sender, args) =>
{
    Console.WriteLine(args.EventName);
    for (int index = 0; index < args.PayloadNames?.Count; index++)
    {
        Console.WriteLine($"\t{args.PayloadNames[index]} = {args.Payload?[index]}");
    }
};


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var app = builder.Build();
app.Run(HandleAsync);
app.Run();

static Task HandleAsync(HttpContext httpContext)
{   
    if (httpContext.Request.Path == new PathString("/error"))
    {
        throw new InvalidOperationException("Manually throw exception.");
    }
    return Task.CompletedTask;
}
public class DiagnosticCollector : EventListener { }
