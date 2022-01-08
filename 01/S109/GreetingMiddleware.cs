namespace App
{
    public class GreetingMiddleware
    {
        public GreetingMiddleware(RequestDelegate next) { }
        public Task InvokeAsync(HttpContext context, IGreeter greeter)
        => context.Response.WriteAsync(greeter.Greet(DateTimeOffset.Now));
    }
}