using Microsoft.Extensions.Diagnostics.HealthChecks;

var random = new Random();
var builder = WebApplication.CreateBuilder();
builder.Services.AddHealthChecks().AddCheck(name:"default",check: Check);
var app = builder.Build();
app.UseHealthChecks(path: "/healthcheck");
app.Run();

HealthCheckResult Check()
=> random!.Next(1, 4) switch
{
    1 => HealthCheckResult.Unhealthy(),
    2 => HealthCheckResult.Degraded(),
    _ => HealthCheckResult.Healthy(),
};
