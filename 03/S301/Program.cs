using App;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var provider = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddScoped<IBar>(_ => new Bar())
    .AddSingleton<IBaz, Baz>()
    .BuildServiceProvider();
Debug.Assert(provider.GetService<IFoo>() is Foo);
Debug.Assert(provider.GetService<IBar>() is Bar);
Debug.Assert(provider.GetService<IBaz>() is Baz);
