var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Services
    .AddSingleton<Foo>()
    .AddScoped<Bar>()
    .AddTransient<Baz>();

var app = builder.Build();
app.Run(InvokeAsync);
app.Run();

static Task InvokeAsync(HttpContext httpContext)
{
    var path = httpContext.Request.Path;
    var requestServices = httpContext.RequestServices;
    Console.WriteLine($"Receive request to {path}");

    requestServices.GetRequiredService<Foo>();
    requestServices.GetRequiredService<Bar>();
    requestServices.GetRequiredService<Baz>();

    requestServices.GetRequiredService<Foo>();
    requestServices.GetRequiredService<Bar>();
    requestServices.GetRequiredService<Baz>();

    if (path == "/stop")
    {
        requestServices.GetRequiredService<IHostApplicationLifetime>()
            .StopApplication();
    }
    return httpContext.Response.WriteAsync("OK");
}

public class Base : IDisposable
{
    public Base() => Console.WriteLine($"{GetType().Name} is created.");
    public void Dispose() => Console.WriteLine($"{GetType().Name} is disposed.");
}
public class Foo : Base { }
public class Bar : Base { }
public class Baz : Base { }
