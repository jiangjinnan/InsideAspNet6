using App;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGreeter, Greeter>();
var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();