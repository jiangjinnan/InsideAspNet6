using Polly;
using Polly.Extensions.Http;
using System.Diagnostics;

var app = WebApplication.Create(args);
var counter = 0;
app.MapGet("/", (HttpResponse response) => response.StatusCode = counter++ % 3 == 0 ? 200 : 500);
await app.StartAsync();

var services = new ServiceCollection();
services
    .AddHttpClient(string.Empty)
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(1)));
var httpClientFactory = services
    .BuildServiceProvider()
    .GetRequiredService<IHttpClientFactory>();

while (true)
{
    var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000");
    var response = await httpClientFactory.CreateClient().SendAsync(request);
    Debug.Assert(response.IsSuccessStatusCode);
}
