using App;
using Microsoft.Extensions.DependencyInjection;

new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddTransient<IBar, Bar>()
    .AddTransient<IQux, Qux>()
    .BuildServiceProvider()
    .GetServices<IQux>();