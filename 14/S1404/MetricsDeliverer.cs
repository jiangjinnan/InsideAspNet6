using Microsoft.Extensions.Options;

namespace App
{
    public class MetricsDeliverer : IMetricsDeliverer
    {
        private readonly TransportType _transport;
        private readonly Endpoint _deliverTo;

        public MetricsDeliverer(IOptions<MetricsCollectionOptions> optionsAccessor)
        {
            var options = optionsAccessor.Value;
            _transport = options.Transport;
            _deliverTo = options.DeliverTo;
        }

        public Task DeliverAsync(PerformanceMetrics counter)
        {
            Console.WriteLine($"[{DateTimeOffset.Now}]Deliver performance counter {counter} to { _deliverTo} via { _transport}");
            return Task.CompletedTask;
        }
    }

}