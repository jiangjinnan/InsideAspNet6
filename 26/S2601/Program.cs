using App;
var builder = WebApplication.CreateBuilder(args);
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
    .UseConcurrencyLimiter()
    .Run(httpContext => Task.Delay(1000).ContinueWith(_ => httpContext.Response.StatusCode = 200));
app.Run();
