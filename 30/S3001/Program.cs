var builder = WebApplication.CreateBuilder();
builder.Services.AddHealthChecks();
var app = builder.Build();
app.UseHealthChecks(path: "/healthcheck");
app.Run();
