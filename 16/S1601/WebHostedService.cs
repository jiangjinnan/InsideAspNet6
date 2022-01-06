using Microsoft.Extensions.Hosting;

namespace App
{
    public class WebHostedService : IHostedService
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;

        public WebHostedService(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
            => _server.StartAsync(_handler);
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
