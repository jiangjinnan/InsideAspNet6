using App;
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter, Greeter>()
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));
var app = builder.Build();
app.MapGet("/greet", Greet);
app.Run();

static string Greet(IGreeter greeter) => greeter.Greet(DateTimeOffset.Now);