using App;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var serviceProviderr = new ServiceCollection()
    .AddSingleton<Foo>()
    .AddSingleton<Bar>()
    .BuildServiceProvider();
var foobar = ActivatorUtilities.CreateInstance<Foobar>(serviceProviderr, "foobar");
Debug.Assert(foobar.Name == "foobar");
