var app = WebApplication.Create();
app.Urls.Add("http://0.0.0.0:6666");
app.Urls.Add("http://0.0.0.0:9999");
app
    .MapHost("*.artech.com")
    .MapHost("www.foo.artech.com")
    .MapHost("www.foo.artech.com:9999");
app.Run();

internal static class Extensions
{
    public static IEndpointRouteBuilder MapHost(this IEndpointRouteBuilder endpoints, string host)
    {
        endpoints.MapGet("/", context => context.Response.WriteAsync(host)).RequireHost(host);
        return endpoints;
    }
}
