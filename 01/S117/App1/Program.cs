using Dapr.Client;
using Shared;

HttpClient client = DaprClient.CreateInvokeHttpClient(appId: "app2");
var input = new Input { X = 2, Y = 1 };

await InvokeAsync("add", "+");
await InvokeAsync("sub", "-");
await InvokeAsync("mul", "*");
await InvokeAsync("div", "/");

async Task InvokeAsync(string method, string @operator)
{
    var response = await client.PostAsync(method, JsonContent.Create(input));
    var output = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<Output>();
    Console.WriteLine($"{input.X} {@operator} {input.Y} = {output!.Result} ({output.Timestamp})");
}