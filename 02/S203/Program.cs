using App;
using System.Diagnostics;

var services = new Cat()
    .Register<Base, Foo>(Lifetime.Transient)
    .Register<Base, Bar>(Lifetime.Transient)
    .Register<Base, Baz>(Lifetime.Transient)
    .GetServices<Base>();
Debug.Assert(services.OfType<Foo>().Any());
Debug.Assert(services.OfType<Bar>().Any());
Debug.Assert(services.OfType<Baz>().Any());

