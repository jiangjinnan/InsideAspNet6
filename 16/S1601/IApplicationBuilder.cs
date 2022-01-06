namespace App
{
    public interface IApplicationBuilder
    {
        RequestDelegate Build();
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
    }

    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _middlewares
            = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            RequestDelegate next = context =>
            {
                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            };
            foreach (var middleware in _middlewares.Reverse())
            {
                next = middleware.Invoke(next);
            }
            return next;
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
