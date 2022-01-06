using Server;

var app = WebApplication.CreateBuilder().Build();
app
    .UsePathBase("/files")
    .UseMiddleware<HttpFileSystemMiddleware>(@"c:\test");
app.Run();
