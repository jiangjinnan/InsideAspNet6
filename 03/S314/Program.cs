using App;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<Foo>()
    .AddSingleton<Bar>()
    .AddSingleton<Baz>()
    .BuildServiceProvider();

ActivatorUtilities.CreateInstance<Foobar>(serviceProvider);
ActivatorUtilities.CreateInstance<BarBaz>(serviceProvider);


