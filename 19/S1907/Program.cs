using Microsoft.AspNetCore.StaticFiles;

var contentTypeProvider = new FileExtensionContentTypeProvider();
contentTypeProvider.Mappings.Add(".img", "image/jpg");
var options = new StaticFileOptions
{
    ContentTypeProvider = contentTypeProvider
};

var app = WebApplication.Create();
app.UseStaticFiles(options);

app.Run();