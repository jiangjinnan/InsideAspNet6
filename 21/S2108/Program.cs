using Microsoft.AspNetCore.Diagnostics;
var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<IDeveloperPageExceptionFilter, FakeExceptionFilter>();
var app = builder.Build();
app.UseDeveloperExceptionPage();
app.MapGet("/", void () => throw new InvalidOperationException("Manually thrown exception..."));
app.Run();

public class FakeExceptionFilter : IDeveloperPageExceptionFilter
{
    public Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
        => errorContext.HttpContext.Response.WriteAsync("Unhandled exception occurred!");
}
