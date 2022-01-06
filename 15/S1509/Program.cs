using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<FoobarMiddleware>()
    .AddSingleton<Foo>()
    .AddSingleton<Bar>();
var app = builder.Build();
app.UseMiddleware<FoobarMiddleware>();
app.Run();

public class FoobarMiddleware : IMiddleware
{
    public FoobarMiddleware(Foo foo, Bar bar)
    {
        Debug.Assert(foo != null);
        Debug.Assert(bar != null);
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Debug.Assert(next != null);
        return Task.CompletedTask;
    }
}

public class Foo { }
public class Bar { }

