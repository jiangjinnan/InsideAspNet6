using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.Tracing;

var listener = new FoobarEventListener();
listener.EventSourceCreated += (sender, args) =>
{
    if (args.EventSource?.Name == "Microsoft-Extensions-Logging")
    {
        listener.EnableEvents(args.EventSource, EventLevel.LogAlways);
    }
};

listener.EventWritten += (sender, args) =>
{
    var payload = args.Payload;
    var payloadNames = args.PayloadNames;
    if (args.EventName == "FormattedMessage" && payload != null && payloadNames !=null)
    {
        var indexOfLevel = payloadNames.IndexOf("Level");
        var indexOfCategory = payloadNames.IndexOf("LoggerName");
        var indexOfEventId = payloadNames.IndexOf("EventId");
        var indexOfMessage = payloadNames.IndexOf("FormattedMessage");
        Console.WriteLine($"{(LogLevel)payload[indexOfLevel]!,-11}: { payload[indexOfCategory]}[{ payload[indexOfEventId]}]");
        Console.WriteLine($"{"",-13}{payload[indexOfMessage]}");
    }
};

var logger = new ServiceCollection()
    .AddLogging(builder => builder
        .AddTraceSource(new SourceSwitch("default", "All"), new DefaultTraceListener { LogFileName = "trace.log" })
        .AddEventSourceLogger())
    .BuildServiceProvider()
    .GetRequiredService<ILogger<Program>>();

var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
levels = levels.Where(it => it != LogLevel.None).ToArray();
var eventId = 1;
Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {level} log message.", level));

internal class FoobarEventListener : EventListener
{ }