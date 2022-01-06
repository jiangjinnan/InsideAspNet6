var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<StringContentMiddleware>(new StringContentMiddleware("Hello World!"));
var app = builder.Build();
app.UseMiddleware<StringContentMiddleware>();
app.Run();

public sealed class StringContentMiddleware : IMiddleware
{
    private readonly string _contents;
    public StringContentMiddleware(string contents)
        => _contents = contents;
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
        => context.Response.WriteAsync(_contents);
}
