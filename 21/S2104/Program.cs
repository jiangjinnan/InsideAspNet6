var app = WebApplication.Create();
app.UseExceptionHandler("/error");
app.MapGet("/", void () => throw new InvalidOperationException("Manually thrown exception"));
app.MapGet("/error", HandleErrorAsync);
app.Run();

static Task HandleErrorAsync(HttpContext context)
    => context.Response.WriteAsync("Unhandled exception occurred!");

