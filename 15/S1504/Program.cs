var app = WebApplication.Create(args);
app.Run(context => context.Response.WriteAsync("Hello World!"));
app.Run();