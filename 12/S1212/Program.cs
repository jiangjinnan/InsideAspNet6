using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;

var httpClient = new ServiceCollection()
    .Configure<HttpClientFactoryOptions>(options => options.ShouldRedactHeaderValue = ShouldRedact)
    .AddLogging(logging => logging
        .AddFilter(level => level == LogLevel.Trace)
        .AddConsole()
        .AddSimpleConsole(options => options.IncludeScopes = true))
    .AddHttpClient()
    .BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>()
    .CreateClient();
var request = new HttpRequestMessage(HttpMethod.Get, "http://www.baidu.com");
request.Headers.Add("Foo", "123");
request.Headers.Add("Bar", "456");
request.Headers.Add("Baz", "789");
await httpClient.SendAsync(request);

static bool ShouldRedact(string headerName) => headerName != "Foo" && headerName != "Bar";
