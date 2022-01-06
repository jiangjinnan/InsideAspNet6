using App;
using Microsoft.Extensions.Logging;

File.AppendAllLines("log.csv", new string[] { $"EventName, Payload, ActivityId, RelatedActivityId" });
var listener = new LoggingEventListener();
var logger = LoggerFactory
    .Create(builder => builder.AddEventSourceLogger())
    .CreateLogger<Program>();

using (logger.BeginScope(new List<KeyValuePair<string, object>> { new("op", "Foo") }))
{
    logger.LogInformation("This is a test log written in scope 'Foo'");
    using (logger.BeginScope(new List<KeyValuePair<string, object>> { new("op", "Bar") }))
    {
        logger.LogInformation("This is a test log written in scope 'Bar'");
    }
    using (logger.BeginScope(new List<KeyValuePair<string, object>> { new("op", "Foo") }))
    {
        logger.LogInformation("This is a test log written in scope 'Baz'");
    }
}
