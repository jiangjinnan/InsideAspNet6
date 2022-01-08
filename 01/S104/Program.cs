var app = WebApplication.Create(args);
IApplicationBuilder appBuilder = app;
appBuilder
    .Use(middleware: HelloMiddleware)
    .Use(middleware: WorldMiddleware);
app.Run();

static RequestDelegate HelloMiddleware(RequestDelegate next)
=> async httpContext => {
    await httpContext.Response.WriteAsync("Hello, ");
    await next(httpContext);
};

static RequestDelegate WorldMiddleware(RequestDelegate next)
=> httpContext => httpContext.Response.WriteAsync("World!");