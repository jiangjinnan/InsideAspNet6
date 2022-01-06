using Microsoft.AspNetCore.Diagnostics;
var random = new Random();
var app = WebApplication.Create();
app.UseStatusCodePages(HandleErrorAsync);
app.MapGet("/", void (HttpResponse response) => response.StatusCode = random.Next(400,599));
app.Run();

static  Task HandleErrorAsync(StatusCodeContext context)
{
    var response = context.HttpContext.Response;
    return response.StatusCode < 500
    ? response.WriteAsync($"Client error ({response.StatusCode})")
    : response.WriteAsync($"Server error ({response.StatusCode})");
}
