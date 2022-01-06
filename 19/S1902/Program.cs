using Microsoft.Extensions.FileProviders;

var path = Path.Combine(Directory.GetCurrentDirectory(), "doc");
var options = new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(path),
    RequestPath = "/documents"
};

var app = WebApplication.Create();
app
    .UseStaticFiles()
    .UseStaticFiles(options);
app.Run();