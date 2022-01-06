var random = new Random();
var app = WebApplication.Create();
app.UseStatusCodePages(app2 => app2.Run(HandleErrorAsync));
app.MapGet("/", void (HttpResponse response) => response.StatusCode = random.Next(400, 599));
app.Run();

static Task HandleErrorAsync(HttpContext context)
{
    var response = context.Response;
    return response.StatusCode < 500
    ? response.WriteAsync($"Client error ({response.StatusCode})")
    : response.WriteAsync($"Server error ({response.StatusCode})");
}
