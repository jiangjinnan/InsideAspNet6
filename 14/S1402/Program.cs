using App;

var collector = new MetricsCollector();
Host.CreateDefaultBuilder()
    .ConfigureServices(svcs => svcs
        .AddHostedService<PerformanceMetricsCollector>()
        .AddSingleton<IProcessorMetricsCollector>(collector)
        .AddSingleton<IMemoryMetricsCollector>(collector)
        .AddSingleton<INetworkMetricsCollector>(collector)
        .AddSingleton<IMetricsDeliverer, MetricsDeliverer>())
    .Build()
    .Run();
