using Microsoft.Extensions.Options;

namespace App
{
    public class MetricsDeliverer : IMetricsDeliverer
    {
        private readonly TransportType _transport;
        private readonly Endpoint _deliverTo;
        private readonly ILogger _logger;
        private readonly Action<ILogger, DateTimeOffset, PerformanceMetrics, Endpoint, TransportType, Exception?> _logForDelivery;

        public MetricsDeliverer(
            IOptions<MetricsCollectionOptions> optionsAccessor,
            ILogger<MetricsDeliverer> logger)
        {
            var options = optionsAccessor.Value;
            _transport = options.Transport;
            _deliverTo = options.DeliverTo;
            _logger = logger;
            _logForDelivery = LoggerMessage.Define<DateTimeOffset, PerformanceMetrics,
                Endpoint, TransportType>(LogLevel.Information, 0,"[{0}]Deliver performance counter {1} to {2} via {3}");
        }

        public Task DeliverAsync(PerformanceMetrics counter)
        {
            _logForDelivery(_logger, DateTimeOffset.Now, counter, _deliverTo, _transport, null);
            return Task.CompletedTask;
        }
    }
}