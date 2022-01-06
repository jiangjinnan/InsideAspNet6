using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(kestrel =>
{
    kestrel.Listen(IPAddress.Any, 80);
    kestrel.Listen(IPAddress.Any, 443, listener => listener.UseHttps(
       https => https.ServerCertificateSelector = SelelctCertificate));
});

builder.Services.AddHttpsRedirection(options => options.HttpsPort = 443);

var app = builder.Build();
app
    .UseHttpsRedirection()
    .Run(httpContext => httpContext.Response.WriteAsync(httpContext.Request.Scheme));
app.Run();

static X509Certificate2? SelelctCertificate(ConnectionContext? context,string? domain)
    => domain?.ToLowerInvariant() switch
    {
        "artech.com" => CertificateLoader.LoadFromStoreCert("artech.com", "My", StoreLocation.CurrentUser, true),
        "blog.artech.com" => CertificateLoader.LoadFromStoreCert("blog.artech.com", "My", StoreLocation.CurrentUser, true),
        "foobar.com" => CertificateLoader.LoadFromStoreCert("foobar.com", "My", StoreLocation.CurrentUser, true),
        _ => throw new InvalidOperationException($"Invalid domain '{domain}'.")
    };
