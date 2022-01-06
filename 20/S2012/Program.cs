using Microsoft.AspNetCore.Mvc;
var app = WebApplication.Create();
app.MapPost("/{foo}", Handle);
app.Run();

static object Handle(
    [FromRoute] string foo,
    [FromQuery] int bar,
    [FromHeader] string host,
    [FromBody] Point point,
    [FromServices] IHostEnvironment environment)
=> new { Foo = foo, Bar = bar, Host = host, Point = point, Environment = environment.EnvironmentName };

public class Point
{ 
    public int X { get; set; }
    public int Y { get; set; }
}