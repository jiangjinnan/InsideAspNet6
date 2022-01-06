using App;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Linq;

var services = new ServiceCollection()
    .AddTransient<Base, Foo>()
    .AddTransient<Base, Bar>()
    .AddTransient<Base, Baz>()
    .BuildServiceProvider()
    .GetServices<Base>();
Debug.Assert(services.OfType<Foo>().Any());
Debug.Assert(services.OfType<Bar>().Any());
Debug.Assert(services.OfType<Baz>().Any());