using App;
using Microsoft.Extensions.DependencyInjection;

new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddTransient<IBar, Bar>()
    .AddTransient<IBaz, Baz>()
    .AddTransient<IQux, Qux>()
    .BuildServiceProvider()
    .GetServices<IQux>();