using App;

var requestId = 1;
var @lock = new object();

var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();
builder.Services
    .AddHostedService<ConsumerHostedService>()
    .AddQueuePolicy(options =>
    {
        options.MaxConcurrentRequests = 2;
        options.RequestQueueLimit = 2;
    });
var app = builder.Build();
app
    .Use(InstrumentAsync)
    .UseConcurrencyLimiter()
    .Run(httpContext => Task.Delay(1000).ContinueWith(_ => httpContext.Response.StatusCode = 200));
await app.StartAsync();

var tasks = Enumerable.Range(1, 5).Select(_ => new HttpClient().GetAsync("http://localhost:5000"));
await Task.WhenAll(tasks);
Console.Read();

async Task InstrumentAsync(HttpContext httpContext, RequestDelegate next)
{    
    Task task;
    int id;
    lock (@lock!)
    {
        id = requestId++;
        task = next(httpContext);
    }
    await task;
    Console.WriteLine($"Request {id}: {httpContext.Response.StatusCode}");
}

