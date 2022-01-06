var app = WebApplication.Create();
app.MapGet("/foo", BuildHandler(app, false));
app.MapGet("/bar", BuildHandler(app, true));
app.Run();

static RequestDelegate BuildHandler(IEndpointRouteBuilder endpoints, bool allowStatusCode404Response)
{
    var options = new ExceptionHandlerOptions
    {
        ExceptionHandler = httpContext =>
        {
            httpContext.Response.StatusCode = 404;
            return Task.CompletedTask;
        },
        AllowStatusCode404Response = allowStatusCode404Response
    };

    var app = endpoints.CreateApplicationBuilder();
    app
        .UseExceptionHandler(options)
        .Run(httpContext => Task.FromException(new InvalidOperationException("Manually thrown exception.")));
    return app.Build();
}
