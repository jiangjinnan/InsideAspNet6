using System.Diagnostics;

var app = WebApplication.Create(args);
app.MapGet("/", () => "Hello World!");
await app.StartAsync();

var serviceProvider = new ServiceCollection()
    .AddHttpClient()
    .BuildServiceProvider();

while (true)
{
    try
    {
        var reply = await serviceProvider.GetRequiredService<HttpClient>().GetStringAsync("http://localhost:5000");
        Debug.Assert(reply == "Hello World!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
