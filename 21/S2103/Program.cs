var app = WebApplication.Create();
app.UseExceptionHandler(app2 => app2.Run(HandleErrorAsync));
app.MapGet("/",  void () => throw new InvalidOperationException("Manually thrown exception"));
app.Run();

static Task HandleErrorAsync(HttpContext context)
    => context.Response.WriteAsync("Unhandled exception occurred!");

