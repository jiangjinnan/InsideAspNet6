using Microsoft.AspNetCore.Mvc;
using Shared;
var app = WebApplication.Create(args);
app.MapPost("{method}", Calculate);
app.Run("http://localhost:9999");

static IResult Calculate(string method, [FromBody] Input input)
{
    var result = method.ToLower() switch
    {
        "add" => input.X + input.Y,
        "sub" => input.X - input.Y,
        "mul" => input.X * input.Y,
        "div" => input.X / input.Y,
        _ => throw new InvalidOperationException($"Invalid operation {method}")
    };
    return Results.Json(new Output { Result = result });
}