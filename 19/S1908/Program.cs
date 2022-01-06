using App;

var options = new DirectoryBrowserOptions
{
    Formatter = new ListDirectoryFormatter()
};


var app = WebApplication.Create();
app.UseDirectoryBrowser(options);

app.Run();