using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Diagnostics;
using System.Text;

var app = WebApplication.Create(args);
app.Run(HandleAsync);
app.Run();

static Task HandleAsync(HttpContext httpContext)
{
    var request = httpContext.Request;
    var configuration = httpContext.RequestServices.GetRequiredService<IConfiguration>();
    var builder = new StringBuilder();
    builder.AppendLine($"Process: {Process.GetCurrentProcess().ProcessName}");
    builder.AppendLine($"MS-ASPNETCORE-TOKEN: {request.Headers["MS-ASPNETCORE-TOKEN"]}");
    builder.AppendLine($"PathBase: {request.PathBase}");
    builder.AppendLine("Environment Variables");
    foreach (string key in Environment.GetEnvironmentVariables().Keys)
    {
        if (key.StartsWith("ASPNETCORE_"))
        {
            builder.AppendLine($"\t{key}={Environment.GetEnvironmentVariable(key)}");
        }
    }
    return httpContext.Response.WriteAsync(builder.ToString());
}




//
// Summary:
//     The minimum data rate for incoming connections.
