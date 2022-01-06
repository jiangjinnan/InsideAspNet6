using App;

Host.CreateDefaultBuilder(args)
    .
    .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
    .Build()
    .Run();
