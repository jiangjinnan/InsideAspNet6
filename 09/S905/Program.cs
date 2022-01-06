using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var logger = LoggerFactory.Create(Configure).CreateLogger<Program>();

var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
levels = levels.Where(it => it != LogLevel.None).ToArray();
var eventId = 1;
Array.ForEach(levels, Log);
Console.Read();

void Configure(ILoggingBuilder builder)
{
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build()
        .GetSection("Logging");
    builder
       .AddConfiguration(configuration)
       .AddConsole()
       .AddConsoleFormatter<TemplatedConsoleFormatter, TemplatedConsoleFormatterOptions>();
}

void Log(LogLevel logLevel) => logger.Log(logLevel, eventId++,
"This is a/an {0} log message.", logLevel);
