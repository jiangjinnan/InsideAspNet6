using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

var logger = new ServiceCollection()
    .AddLogging(builder => builder
        .Configure(options => options.ActivityTrackingOptions =
             ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId |
             ActivityTrackingOptions.ParentId)
        .AddConsole()
        .AddSimpleConsole(options => options.IncludeScopes = true))
    .BuildServiceProvider()
    .GetRequiredService<ILogger<Program>>();

ActivitySource.AddActivityListener(new ActivityListener { ShouldListenTo = _ => true, Sample = Sample });
var source = new ActivitySource("App");
using (source.StartActivity("Foo"))
{
    logger.Log(LogLevel.Information, "This is a log written in scope Foo.");
    using (source.StartActivity("Bar"))
    {
        logger.Log(LogLevel.Information, "This is a log written in scope Bar.");
        using (source.StartActivity("Baz"))
        {
            logger.Log(LogLevel.Information, "This is a log written in scope Baz.");
        }
    }
}
Console.Read();

static ActivitySamplingResult Sample(ref ActivityCreationOptions<ActivityContext> options)
    => ActivitySamplingResult.AllData;
