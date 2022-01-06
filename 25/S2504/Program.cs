using Microsoft.AspNetCore.Rewrite;

var app = WebApplication.Create();
var options = new RewriteOptions().AddApacheModRewrite(fileProvider: app.Environment.ContentRootFileProvider, filePath: "rewrite.config");
app.UseRewriter(options);
app.MapGet("/{**foobar}", (HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}");
app.Run();

