using Microsoft.AspNetCore.Rewrite;
var app = WebApplication.Create();
var options = new RewriteOptions().AddRedirect("^foo/(.*)", "bar/$1");
app.UseRewriter(options);
app.MapGet("/{**foobar}", (HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}");
app.Run();
