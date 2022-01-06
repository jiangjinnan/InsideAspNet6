using System.Diagnostics;

var app = WebApplication.Create(args);
app.MapGet("/", () => "Hello World!");
await app.StartAsync();

while (true)
{
    using (var httpClient = new HttpClient())
    {
        try
        {
            var reply = await httpClient.GetStringAsync("http://localhost:5000");
            Debug.Assert(reply == "Hello World!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
