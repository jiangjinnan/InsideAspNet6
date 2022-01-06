using App;
using System.Diagnostics;

var app = WebApplication.Create(args);
app.Urls.Add("http://0.0.0.0:80");
app.MapGet("/{path}", (HttpRequest resquest, HttpResponse response)
   => response.WriteAsync($"{resquest.Host}{resquest.Path}"));
await app.StartAsync();

var services = new ServiceCollection();
services.AddHttpClient<FooClient>("foo", httpClient
    => httpClient.BaseAddress = new Uri("http://www.foo.com"));
services.AddHttpClient<BarClient>("bar", httpClient
    => httpClient.BaseAddress = new Uri("http://www.bar.com"));
var serviceProvider = services.BuildServiceProvider();
var foo = serviceProvider.GetRequiredService<FooClient>();
var bar = serviceProvider.GetRequiredService<BarClient>();

var reply = await foo.GetStringAsync("abc");
Debug.Assert(reply == "www.foo.com/abc");

reply = await bar.GetStringAsync("xyz");
Debug.Assert(reply == "www.bar.com/xyz");

