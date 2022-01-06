using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("logging.json")
    .Build();

var loggerFactory = new ServiceCollection()
    .AddLogging(builder => builder
        .AddConfiguration(configuration)
        .AddConsole()
        .AddDebug())
    .BuildServiceProvider()
    .GetRequiredService<ILoggerFactory>();


Log(loggerFactory, "Foo");
Log(loggerFactory, "Bar");
Log(loggerFactory, "Baz");

Console.Read();

static void Log(ILoggerFactory loggerFactory, string category)
{
    var logger = loggerFactory.CreateLogger(category);
    var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
    levels = levels.Where(it => it != LogLevel.None).ToArray();
    var eventId = 1;
    Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {0} log message.", level));
}