var app = WebApplication.CreateBuilder().Build();
app
    .UseMiddleware<StringContentMiddleware>("Hello")
    .UseMiddleware<StringContentMiddleware>(" World!", false);
app.Run();

public sealed class StringContentMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _contents;
    private readonly bool _forewardToNext;

    public StringContentMiddleware(RequestDelegate next, string contents, bool forewardToNext = true)
    {
        _next = next;
        _forewardToNext = forewardToNext;
        _contents = contents;
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.WriteAsync(_contents);
        if (_forewardToNext)
        {
            await _next(context);
        }
    }
}



