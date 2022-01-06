using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Text.Json.Serialization;

var random = new Random();
var options = new HealthCheckOptions
{
    ResponseWriter = ReportAsync
};

var builder = WebApplication.CreateBuilder();
builder.Services.AddHealthChecks()
    .AddCheck(name: "foo", check: Check,tags: new string[] { "foo1", "foo2" })
    .AddCheck(name: "bar", check: Check, tags: new string[] { "bar1", "bar2" })
    .AddCheck(name: "baz", check: Check, tags: new string[] { "baz1", "baz2" });

var app = builder.Build();
app.UseHealthChecks(path: "/healthcheck", options: options);
app.Run();

static Task ReportAsync(HttpContext context, HealthReport report)
{
    context.Response.ContentType = "application/json";
    var options = new JsonSerializerOptions();
    options.WriteIndented = true;
    options.Converters.Add(new JsonStringEnumConverter());
    return context.Response.WriteAsync(JsonSerializer.Serialize(report, options));
}

HealthCheckResult Check() => random!.Next(1, 4) switch
{
    1 => HealthCheckResult.Unhealthy(),
    2 => HealthCheckResult.Degraded(),
    _ => HealthCheckResult.Healthy(),
};
