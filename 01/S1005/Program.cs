var app = WebApplication.Create(args);
app .Use(middleware: HelloMiddleware)
    .Use(middleware: WorldMiddleware);
app.Run();

static async Task HelloMiddleware(HttpContext httpContext, RequestDelegate next)
{ 
    await httpContext.Response.WriteAsync("Hello, ");
    await next(httpContext);
};

static Task WorldMiddleware(HttpContext httpContext, RequestDelegate next)
=> httpContext.Response.WriteAsync("World!");