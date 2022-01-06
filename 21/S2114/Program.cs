using Microsoft.AspNetCore.Diagnostics;

var random = new Random();
var app = WebApplication.Create();
app.UseStatusCodePages(HandleAsync);
app.MapGet("/", Process);
app.Run();

static Task HandleAsync(StatusCodeContext context)
    => context.HttpContext.Response.WriteAsync("Error occurred!");

void  Process(HttpContext context)
{
    context.Response.StatusCode = 401;
    if (random.Next() % 2 == 0)
    {
        context.Features.Get<IStatusCodePagesFeature>()!.Enabled = false;
    }
}

