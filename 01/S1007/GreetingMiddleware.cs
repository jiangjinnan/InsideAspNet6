namespace App
{
public class GreetingMiddleware : IMiddleware
{
    private readonly IGreeter _greeter;
    public GreetingMiddleware(IGreeter greeter)=> _greeter = greeter;
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    => context.Response.WriteAsync(_greeter.Greet(DateTimeOffset.Now));
}
}