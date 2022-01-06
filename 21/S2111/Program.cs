using Microsoft.AspNetCore.Diagnostics;

var app = WebApplication.Create();
app.UseExceptionHandler("/error");
app.MapGet("/error", HandleError);
app.MapGet("/", void () => throw new InvalidOperationException("Manually thrown exception"));
app.Run();

static IResult HandleError(HttpContext context)
{
    var ex = context.Features.Get<IExceptionHandlerPathFeature>()!.Error;
    var html = $@"
<html>
    <head><title>Error</title></head>
    <body>
        <h3>{ex.Message}</h3>
        <p>Type: {ex.GetType().FullName}</p>
        <p>StackTrace: {ex.StackTrace}</p>
    </body>
</html>";
    return Results.Content(html, "text/html");
}
