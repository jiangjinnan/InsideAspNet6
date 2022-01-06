using Microsoft.Extensions.FileProviders;

var path = Path.Combine(Directory.GetCurrentDirectory(), "doc");
var fileProvider = new PhysicalFileProvider(path);
var fileOptions = new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = "/documents"
};

var diretoryOptions = new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = "/documents"
};

var app = WebApplication.Create();
app
    .UseStaticFiles()
    .UseStaticFiles(fileOptions)
    .UseDirectoryBrowser()
    .UseDirectoryBrowser(diretoryOptions);

app.Run();
