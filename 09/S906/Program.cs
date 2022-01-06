using App;
using Microsoft.Extensions.Logging;

var listener = new LoggingEventListener();
var logger = LoggerFactory
    .Create(builder => builder.AddEventSourceLogger())
    .CreateLogger<Program>();

var state = new Dictionary<string, object>
{
    ["ErrorCode"] = 100,
    ["Message"] = "Unhandled exception"
};

logger.Log(LogLevel.Error, 1, state, new InvalidOperationException("This is a manually thrown exception."), (_, ex) => ex?.Message??"Error");
Console.Read();
