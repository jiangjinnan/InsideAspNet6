using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

var logger = new ServiceCollection()
    .AddLogging(builder => builder
        .AddFilter(Filter)
        .AddConsole()
        .AddDebug())
    .BuildServiceProvider()
    .GetRequiredService<ILogger<Program>>();

var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
levels = levels.Where(it => it != LogLevel.None).ToArray();
var eventId = 1;
Array.ForEach(levels, level => logger.Log(level, eventId++,"This is a/an {0} log message.", level));
Console.Read();

static bool Filter(string provider, string category, LogLevel level)
=> provider switch
{
    var p when p == typeof(ConsoleLoggerProvider).FullName => level >= LogLevel.Debug,
    var p when p == typeof(DebugLoggerProvider).FullName => level >= LogLevel.Warning,
    _ => true,
};