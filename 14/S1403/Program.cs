using App;

var collector = new MetricsCollector();
Host.CreateDefaultBuilder()
    .ConfigureServices((context, svcs) => svcs
        .AddHostedService<PerformanceMetricsCollector>()
        .AddSingleton<IProcessorMetricsCollector>(collector)
        .AddSingleton<IMemoryMetricsCollector>(collector)
        .AddSingleton<INetworkMetricsCollector>(collector)
        .AddSingleton<IMetricsDeliverer, MetricsDeliverer>()
        .Configure<MetricsCollectionOptions>(context.Configuration.GetSection("MetricsCollection")))
    .Build()
    .Run();
