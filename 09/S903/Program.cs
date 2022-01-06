using Microsoft.Extensions.Logging;

var includeScopes = args.Contains("includeScopes");

var logger = LoggerFactory
    .Create(builder => builder
        .AddConsole()
        .AddJsonConsole(options => options.IncludeScopes = includeScopes))
    .CreateLogger<Program>();

var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
levels = levels.Where(it => it != LogLevel.None).ToArray();
var eventId = 1;
Array.ForEach(levels, Log);

Console.Read();

void Log(LogLevel logLevel)
{
    using (logger.BeginScope("Foo"))
    {
        using (logger.BeginScope("Bar"))
        {
            using (logger.BeginScope("Baz"))
            {
                logger.Log(logLevel, eventId++, new Exception("Error..."), "This is a/an {0} log message.", logLevel);
            }
        }
    }
}
