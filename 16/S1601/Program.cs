using App;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder()
    .ConfigureWebHost(builder => builder
        .UseHttpListenerServer()
        .Configure(app => app
            .Use(FooMiddleware)
            .Use(BarMiddleware)
            .Use(BazMiddleware)))
    .Build()
    .Run();

static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
{
    await context.Response.WriteAsync("Foo=>");
    await next(context);
};

static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
{
    await context.Response.WriteAsync("Bar=>");
    await next(context);
};

 static RequestDelegate BazMiddleware(RequestDelegate next)
 => context => context.Response.WriteAsync("Baz");
