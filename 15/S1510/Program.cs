using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<Foo>()
    .AddSingleton<Bar>();
var app = builder.Build();
app.UseMiddleware<FoobarMiddleware>();
app.Run();

public class FoobarMiddleware
{
    private readonly RequestDelegate _next;
    public FoobarMiddleware(RequestDelegate next) => _next = next;
    public Task InvokeAsync(HttpContext context, Foo foo, Bar bar)
    {
        Debug.Assert(context != null);
        Debug.Assert(foo != null);
        Debug.Assert(bar != null);
        return _next(context);
    }
}

public class Foo { }
public class Bar { }
