var random = new Random();
var app = WebApplication.Create();
app.UseStatusCodePagesWithRedirects("~/error/{0}");
app.Map("/error/{statusCode}", (HttpResponse response, int statusCode) => response.WriteAsync($"Error occurred ({statusCode})"));
app.Map("/", void (HttpResponse response) => response.StatusCode = random.Next(400, 599));
app.Run();

