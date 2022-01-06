using App2;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Shared;
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IResultCache, ResultCache>()
    .AddDaprClient();
var app = builder.Build();

app.UseCloudEvents();
app.MapPost("clear", ClearAsync);
app.MapSubscribeHandler();

app.MapPost("/{method}", Calculate);
app.Run("http://localhost:9999");

[Topic(pubsubName:"pubsub", name:"clearresult")]
static Task ClearAsync(IResultCache cache, [FromBody] string[] methods) => cache.ClearAsync(methods);
static async Task<IResult> Calculate(string method, [FromBody]Input input, IResultCache resultCache)
{
    var output = await resultCache.GetAsync(method, input);
    if (output == null)
    {
        var result = method.ToLower() switch
        {
            "add" => input.X + input.Y,
            "sub" => input.X - input.Y,
            "mul" => input.X * input.Y,
            "div" => input.X / input.Y,
            _ => throw new InvalidOperationException($"Invalid operation {method}")
        };
        output = new Output { Result = result };
        await resultCache.SaveAsync(method, input, output);
    }
    return Results.Json(output);
}