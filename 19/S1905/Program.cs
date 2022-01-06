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
var defaultOptions1 = new DefaultFilesOptions();
var defaultOptions2 = new DefaultFilesOptions
{
    RequestPath = "/documents",
    FileProvider = fileProvider,
};

defaultOptions1.DefaultFileNames.Add("readme.html");
defaultOptions2.DefaultFileNames.Add("readme.html");

var app = WebApplication.Create();
app
    .UseDefaultFiles(defaultOptions1)
    .UseDefaultFiles(defaultOptions2)
    .UseStaticFiles()
    .UseStaticFiles(fileOptions)
    .UseDirectoryBrowser()
    .UseDirectoryBrowser(diretoryOptions);

app.Run();
