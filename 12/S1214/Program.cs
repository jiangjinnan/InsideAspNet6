using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

var services = new ServiceCollection()
    .AddTransient<Foo>()
    .AddScoped<Bar>()
    .Configure<HttpClientFactoryOptions>(options =>
    {
        options.HandlerLifetime = TimeSpan.FromSeconds(2);
        options.HttpMessageHandlerBuilderActions.Add(AddHandler);
        options.SuppressHandlerScope = true;
    });
var httpClientFactory = services
    .AddHttpClient()
    .BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>();

for (int index = 0; index < 3; index++)
{
    await httpClientFactory.CreateClient().GetAsync("http://www.baidu.com");
    await Task.Delay(TimeSpan.FromSeconds(2));
    GC.Collect();
}

Console.Read();

static void AddHandler(HttpMessageHandlerBuilder builder)
{
    builder.AdditionalHandlers.Add(ActivatorUtilities.CreateInstance<FoobarHttpMessageHandler>(builder.Services));
}
