var options = new ExceptionHandlerOptions { ExceptionHandler = HandleErrorAsync };
var app = WebApplication.Create();
app.UseExceptionHandler(options);
app.MapGet("/",  void () => throw new InvalidOperationException("Manually thrown exception"));
app.Run();

static Task HandleErrorAsync(HttpContext context) => context.Response.WriteAsync("Unhandled exception occurred!");

