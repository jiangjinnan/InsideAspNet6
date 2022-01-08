namespace App
{
    public class GreetingMiddleware
    {
        private readonly IGreeter _greeter;
        public GreetingMiddleware(RequestDelegate _, IGreeter greeter)=> _greeter = greeter;
        public Task InvokeAsync(HttpContext context)
        => context.Response.WriteAsync(_greeter.Greet(DateTimeOffset.Now));
    }
}