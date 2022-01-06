using App;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(kestrel => kestrel.ConfigureEndpointDefaults(endpoint => endpoint.Protocols =  HttpProtocols.Http2));
builder.Services.AddGrpc();
var app = builder.Build();
app.MapGrpcService<CalculatorService>();
app.Run();
