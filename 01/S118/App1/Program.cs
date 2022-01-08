using Dapr.Client;
using Shared;

using (var client = DaprClient.CreateInvokeHttpClient(appId: "app2"))
{
    var input = new Input { X = 2, Y = 1 };

    await InvokeAsync();
    await Task.Delay(5000);
    Console.WriteLine();
    await InvokeAsync();

    async Task InvokeAsync()
    {
        await InvokeCoreAsync("add", "+");
        await InvokeCoreAsync("sub", "-");
        await InvokeCoreAsync("mul", "*");
        await InvokeCoreAsync("div", "/");
    }

    async Task InvokeCoreAsync(string method, string @operator)
    {
        var response = await client.PostAsync(method, JsonContent.Create(input));
        var output = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<Output>();
        Console.WriteLine($"{input.X} {@operator} {input.Y} = {output!.Result} ({output.Timestamp})");
    }
}