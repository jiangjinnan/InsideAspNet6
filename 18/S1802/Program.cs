var builder = WebApplication.CreateBuilder(args);
builder.WebHost
    .UseKestrel(kestrel => kestrel.ListenLocalhost(8000))
    .UseUrls("http://localhost:9000");
var app = builder.Build();
app.Run();




