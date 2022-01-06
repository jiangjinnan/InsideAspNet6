using App;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var services = new ServiceCollection();
services.AddHttpClient(string.Empty)
    .ConfigurePrimaryHttpMessageHandler(_ => new ExtendedHttpClientHandler())
    .AddHttpMessageHandler(_ => new FooHttpMessageHandler())
    .AddHttpMessageHandler(_ => new BarHttpMessageHandler())
    .AddHttpMessageHandler(_ => new BazHttpMessageHandler());

var httpClient = services.BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>()
    .CreateClient();
var handlerField = typeof(HttpMessageInvoker).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance);
PrintPipeline((HttpMessageHandler?)handlerField?.GetValue(httpClient), 0);

static void PrintPipeline(HttpMessageHandler? handler, int index)
{
    if (index == 0)
    {
        Console.WriteLine(handler?.GetType().Name);
    }
    else
    {
        Console.WriteLine($"{new string(' ', index * 4)}=>{handler?.GetType().Name}");
    }
    if (handler is DelegatingHandler delegatingHandler)
    {
        PrintPipeline(delegatingHandler.InnerHandler, index + 1);
    }
}
