using App;
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter, Greeter>()
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));
var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();