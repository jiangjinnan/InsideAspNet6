var app = WebApplication.Create(args);
IApplicationBuilder applicationBuilder = app;
applicationBuilder
    .Use(Middleware1)
    .Use(Middleware2);

app.Run();

static RequestDelegate Middleware1(RequestDelegate next)
    => async context =>
    {
        await context.Response.WriteAsync("Hello");
        await next(context);
    };
static RequestDelegate Middleware2(RequestDelegate next)
    => context => context.Response.WriteAsync(" World!");
