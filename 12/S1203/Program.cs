using System.Diagnostics;

var app = WebApplication.Create(args);
app.MapGet("/", () => "Hello World!");
await app.StartAsync();

var httpClientFactory = new ServiceCollection()
    .AddHttpClient()
    .BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>();

while (true)
{
    try
    {
        var reply = await httpClientFactory.CreateClient().GetStringAsync("http://localhost:5000");
        Debug.Assert(reply == "Hello World!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
