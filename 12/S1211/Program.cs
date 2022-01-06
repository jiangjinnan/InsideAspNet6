using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection()
    .AddLogging(logging => logging
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole()
        .AddSimpleConsole(options => options.IncludeScopes = true));
services.AddHttpClient(string.Empty).AddHttpMessageHandler(() => new DelayHttpMessageHanadler());
var httpClient = services
    .BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>()
    .CreateClient();
await httpClient.GetAsync("http://www.baidu.com");