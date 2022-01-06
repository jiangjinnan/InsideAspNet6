using Microsoft.AspNetCore.Session;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Services
    .AddDistributedRedisCache(options => options.Configuration = "localhost")
    .AddSession();
var app = builder.Build();
app.UseSession();
app.MapGet("/{foobar?}", ProcessAsync);
app.Run();

static async ValueTask<IResult> ProcessAsync(HttpContext context)
{
    var session = context.Session;
    await session.LoadAsync();
    string sessionStartTime;
    if (session.TryGetValue("__SessionStartTime", out var value))
    {
        sessionStartTime = Encoding.UTF8.GetString(value);
    }
    else
    {
        sessionStartTime = DateTime.Now.ToString();
        session.SetString("__SessionStartTime", sessionStartTime);
    }
    var field = typeof(DistributedSession).GetTypeInfo().GetField("_sessionKey", BindingFlags.Instance | BindingFlags.NonPublic)!;
    var sessionKey = field.GetValue(session);

    var html = $@"
<html>
    <head><title>Session Demo</title></head>
    <body>
        <ul>
            <li>Session ID:{session.Id}</li>
            <li>Session Start Time:{sessionStartTime}</li>
            <li>Session Key:{sessionKey}</li>
            <li>Current Time:{DateTime.Now}</li>
        <ul>
    </body>
</html>";
    return Results.Content(html, "text/html");
}
