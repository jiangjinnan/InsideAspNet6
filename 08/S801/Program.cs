using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var logger = new ServiceCollection()
    .AddLogging(builder => builder
        .AddConsole()
        .AddDebug())
    .BuildServiceProvider()
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger("Program");

var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
levels = levels.Where(it => it != LogLevel.None).ToArray();
var eventId = 1;
Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {0} log message.", level));
Console.Read();