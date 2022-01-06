var options = new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "image/jpg"
};
var app = WebApplication.Create();
app.UseStaticFiles(options);

app.Run();