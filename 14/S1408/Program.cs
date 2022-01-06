using App;
using System.Reflection;

Host.CreateDefaultBuilder()
    .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
    .UseServiceProviderFactory(new CatServiceProviderFactory())
    .ConfigureContainer<CatBuilder>(builder => builder.Register(Assembly.GetEntryAssembly()!))
    .Build()
    .Run();
