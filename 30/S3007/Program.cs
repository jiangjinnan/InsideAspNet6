using Microsoft.Extensions.Diagnostics.HealthChecks;

var random = new Random();

var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();
builder.Services
    .AddHealthChecks()
    .AddCheck("foo", Check)
    .AddCheck("bar", Check)
    .AddCheck("baz", Check)
    .AddConsolePublisher()
    .ConfigurePublisher(options =>options.Period = TimeSpan.FromSeconds(5));

var app = builder.Build();
app.UseHealthChecks(path: "/healthcheck");
app.Run();

HealthCheckResult Check() => random!.Next(1, 4) switch
{
    1 => HealthCheckResult.Unhealthy(),
    2 => HealthCheckResult.Degraded(),
    _ => HealthCheckResult.Healthy(),
};
