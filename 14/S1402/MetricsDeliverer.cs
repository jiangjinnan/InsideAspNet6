namespace App
{
    public class MetricsDeliverer : IMetricsDeliverer
    {
        public Task DeliverAsync(PerformanceMetrics counter)
        {
            Console.WriteLine($"[{DateTimeOffset.UtcNow}]{counter}");
            return Task.CompletedTask;
        }
    }
}