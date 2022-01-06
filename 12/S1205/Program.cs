using System.Diagnostics;

var app = WebApplication.Create(args);
app.Urls.Add("http://0.0.0.0:80");
app.MapGet("/{path}" , (HttpRequest resquest, HttpResponse response) =>response.WriteAsync($"{resquest.Host}{resquest.Path}"));
await app.StartAsync();

var services = new ServiceCollection();
services.AddHttpClient("foo", httpClient => httpClient.BaseAddress = new Uri("http://www.foo.com"));
services.AddHttpClient("bar", httpClient => httpClient.BaseAddress = new Uri("http://www.bar.com"));
var httpClientFactory = services
    .BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>();

var reply = await httpClientFactory.CreateClient("foo").GetStringAsync("abc");
Debug.Assert(reply == "www.foo.com/abc");
reply = await httpClientFactory.CreateClient("bar").GetStringAsync("xyz");
Debug.Assert(reply == "www.bar.com/xyz");
