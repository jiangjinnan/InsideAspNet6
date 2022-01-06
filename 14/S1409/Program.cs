using App;

Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging=>logging.ClearProviders())
    .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
    .Build()
    .Run();

