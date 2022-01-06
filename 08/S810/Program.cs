using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

var random = new Random();
var template = @"Method FoobarAsync is invoked.
    Arguments: foo={foo}, bar={bar}
    Return value: {returnValue}
    Time:{time}";
var log = LoggerMessage.Define<int, long, double, TimeSpan>(
    logLevel: LogLevel.Information, 
    eventId: 3721, 
    formatString: template);
var logger = new ServiceCollection()
    .AddLogging(builder => builder.AddConsole())
    .BuildServiceProvider()
    .GetRequiredService<ILogger<Program>>();
await FoobarAsync(random.Next(), random.Next());
await FoobarAsync(random.Next(), random.Next());
Console.Read();

async Task<double> FoobarAsync(int foo, long bar)
{
    var stopwatch = Stopwatch.StartNew();
    await Task.Delay(random.Next(100, 900));
    var result = random.Next();
    log(logger, foo, bar, result, stopwatch.Elapsed, null);
    return result;
}