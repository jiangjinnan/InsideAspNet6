var app = App.WebApplication.Create();
app.Run(httpContext => httpContext.Response.WriteAsync("Hello World!"));
app.Run();
