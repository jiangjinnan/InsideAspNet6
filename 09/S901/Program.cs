using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

var configuration = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();
var singleLine = configuration.GetSection("singleLine").Get<bool>();
var colorBebavior = configuration.GetSection("color").Get<LoggerColorBehavior>();

var logger = LoggerFactory.Create(builder => builder
    .AddConsole()
    .AddSimpleConsole(options =>
    {
        options.SingleLine = singleLine;
        options.ColorBehavior = colorBebavior;
    }))
    .CreateLogger<Program>();
var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
levels = levels.Where(it => it != LogLevel.None).ToArray();
var eventId = 1;
Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {0} log message.", level));
Console.Read();
