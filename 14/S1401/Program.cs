using App;

Host.CreateDefaultBuilder()
    .ConfigureServices(svcs => svcs.AddHostedService<PerformanceMetricsCollector>())
    .Build()
    .Run();
