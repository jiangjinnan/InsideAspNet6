using App;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Replace(ServiceDescriptor.Singleton<IServer, HttpListenerServer>());
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run("http://localhost:5000/");
