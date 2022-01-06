var app = WebApplication.Create();
IEndpointRouteBuilder routeBuilder = app;
app.MapGet("/foobar", routeBuilder.CreateApplicationBuilder()
    .Use(FooMiddleware)
    .Use(BarMiddleware)
    .Use(BazMiddleware)
    .Build());
app.Run();

static async Task FooMiddleware(HttpContext context,RequestDelegate next) 
{
    await context.Response.WriteAsync("Foo=>");
    await next(context);
};
static async Task BarMiddleware(HttpContext context, RequestDelegate next) 
{
    await context.Response.WriteAsync("Bar=>");
    await next(context);
};

static Task BazMiddleware(HttpContext context, RequestDelegate next)
=> context.Response.WriteAsync("Baz");

