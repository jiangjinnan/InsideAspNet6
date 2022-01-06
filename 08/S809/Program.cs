using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

var logger = new ServiceCollection()
    .AddLogging(builder => builder
        .AddConsole()
        .AddSimpleConsole(options => options.IncludeScopes = true))
    .BuildServiceProvider()
    .GetRequiredService<ILogger<Program>>();

using (logger.BeginScope($"Foobar Transaction[{Guid.NewGuid()}]"))
{
    var stopwatch = Stopwatch.StartNew();
    await Task.Delay(500);
    logger.LogInformation("Operation foo completes at {0}", stopwatch.Elapsed);

    await Task.Delay(300);
    logger.LogInformation("Operation bar completes at {0}", stopwatch.Elapsed);

    await Task.Delay(800);
    logger.LogInformation("Operation baz completes at {0}", stopwatch.Elapsed);
}
Console.Read();