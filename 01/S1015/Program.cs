using App;
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter, Greeter>()
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"))
    .AddControllersWithViews();
var app = builder.Build();
app.MapControllers();
app.Run();