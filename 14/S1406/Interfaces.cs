namespace App
{
    public interface IProcessorMetricsCollector
    {
        int GetUsage();
    }
    public interface IMemoryMetricsCollector
    {
        long GetUsage();
    }
    public interface INetworkMetricsCollector
    {
        long GetThroughput();
    }

    public interface IMetricsDeliverer
    {
        Task DeliverAsync(PerformanceMetrics counter);
    }

}