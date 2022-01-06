using Microsoft.AspNetCore.Rewrite;
var app = WebApplication.Create();
var options = new RewriteOptions().AddRewrite(regex: "^foo/(.*)", replacement: "bar/$1", skipRemainingRules: true);
app.UseRewriter(options);
app.MapGet("/{**foobar}", (HttpRequest request) =>
    $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}");
app.Run();
