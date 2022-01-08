RequestDelegate handler = context => context.Response.WriteAsync("Hello, World!");
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();
app.Run(handler: handler);
app.Run();
