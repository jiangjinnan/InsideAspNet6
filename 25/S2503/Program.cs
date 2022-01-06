using Microsoft.AspNetCore.Rewrite;

var app = WebApplication.Create();
var options = new RewriteOptions().AddIISUrlRewrite(fileProvider: app.Environment.ContentRootFileProvider, filePath: "rewrite.xml");
app.UseRewriter(options);
app.MapGet("/{**foobar}", (HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}");
app.Run();

