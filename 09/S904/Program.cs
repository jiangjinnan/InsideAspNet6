using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using (var @out = new StreamWriter("out.log") { AutoFlush = true })
using (var error = new StreamWriter("error.log") { AutoFlush = true })
{
    Console.SetOut(@out);
    Console.SetError(error);

    var logger = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddConsole(options =>
                options.LogToStandardErrorThreshold = LogLevel.Error)
            .AddSimpleConsole(options =>
                options.ColorBehavior = LoggerColorBehavior.Disabled))
        .CreateLogger<Program>();

    var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
    levels = levels.Where(it => it != LogLevel.None).ToArray();
    var eventId = 1;
    Array.ForEach(levels, Log);
    Console.Read();

    void Log(LogLevel logLevel) => logger.Log(logLevel, eventId++, "This is a/an {0} log message.", logLevel);
}
