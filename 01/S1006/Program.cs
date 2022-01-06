var app = WebApplication.Create(args);
app 
    .Use(middleware: HelloMiddleware)
    .Use(middleware: WorldMiddleware);
app.Run();

static async Task HelloMiddleware(HttpContext httpContext, Func<Task> next)
{ 
    await httpContext.Response.WriteAsync("Hello, ");
    await next();
};

static Task WorldMiddleware(HttpContext httpContext, Func<Task> next)
=> httpContext.Response.WriteAsync("World!");