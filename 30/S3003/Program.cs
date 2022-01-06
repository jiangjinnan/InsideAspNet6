using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var random = new Random();
var options = new HealthCheckOptions
{
    ResultStatusCodes = new Dictionary<HealthStatus, int>
    {
        [HealthStatus.Healthy] = 299,
        [HealthStatus.Degraded] = 298,
        [HealthStatus.Unhealthy] = 503
    }
};

var builder = WebApplication.CreateBuilder();
builder.Services
    .AddHealthChecks()
    .AddCheck(name:"default",check: Check);
var app = builder.Build();
app.UseHealthChecks(path: "/healthcheck", options: options);
app.Run();

HealthCheckResult Check() => random!.Next(1, 4) switch
{
    1 => HealthCheckResult.Unhealthy(),
    2 => HealthCheckResult.Degraded(),
    _ => HealthCheckResult.Healthy(),
};
