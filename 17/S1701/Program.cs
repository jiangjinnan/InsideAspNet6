var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSimpleConsole(options => options.IncludeScopes = true);
var app = builder.Build();
app.Run(HandleAsync);
app.Run();

static Task HandleAsync(HttpContext httpContext)
{
    var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Log for event Foobar");
    if (httpContext.Request.Path == new PathString("/error"))
    {
        throw new InvalidOperationException("Manually throw exception.");
    }
    return Task.CompletedTask;
}
