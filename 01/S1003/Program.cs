var app = WebApplication.Create(args);
app.Run(handler: HandleAsync);
app.Run();

static Task HandleAsync(HttpContext httpContext) => httpContext.Response.WriteAsync("Hello, World!");
