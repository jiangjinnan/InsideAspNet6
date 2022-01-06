using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var loggerFactory = new ServiceCollection()
    .AddLogging(builder => builder
        .AddFilter(Filter)
        .AddConsole())
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

static bool Filter(string category, LogLevel level)
{
    return category switch
    {
        "Foo" => level >= LogLevel.Debug,
        "Bar" => level >= LogLevel.Warning,
        "Baz" => level >= LogLevel.None,
        _ => level >= LogLevel.Information,
    };
}