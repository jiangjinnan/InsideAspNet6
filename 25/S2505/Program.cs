using Microsoft.AspNetCore.Rewrite;

var app = WebApplication.Create();
app.MapGet("/foo", CreateHandler(app, 302));
app.MapGet("/bar", CreateHandler(app, 307));
app.Run();

static RequestDelegate CreateHandler(IEndpointRouteBuilder endpoints, int statusCode)
{
    var app = endpoints.CreateApplicationBuilder();
    app
        .UseRewriter(new RewriteOptions().AddRedirectToHttps(statusCode, 5001))
        .Run(httpContext => {
            var request = httpContext.Request;
            var address =
            $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}";
            return httpContext.Response.WriteAsync(address);
        });
    return app.Build();
}
