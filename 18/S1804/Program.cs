using App;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder();
builder.WebHost.UseKestrel(kestrel => kestrel.ListenLocalhost(5000));
builder.Services.Replace(ServiceDescriptor.Singleton<IServer, MiniKestrelServer>());
var app = builder.Build();
app.Run(context => context.Response.WriteAsync("Hello World!"));
app.Run();