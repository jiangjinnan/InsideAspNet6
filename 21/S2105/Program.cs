var app = WebApplication.Create();
app.UseStatusCodePages("text/plain", "Error occurred ({0})");
app.MapGet("/", void (HttpResponse response) => response.StatusCode = 500);
app.Run();

