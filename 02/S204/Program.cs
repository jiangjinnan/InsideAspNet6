using App;
using (var root = new Cat()
            .Register<IFoo, Foo>(Lifetime.Transient)
            .Register<IBar>(_ => new Bar(), Lifetime.Self)
            .Register<IBaz, Baz>(Lifetime.Root)
            .Register(typeof(IFoo).Assembly))
{
    using (var cat = root.CreateChild())
    {
        cat.GetService<IFoo>();
        cat.GetService<IBar>();
        cat.GetService<IBaz>();
        cat.GetService<IQux>();
        Console.WriteLine("Child cat is disposed.");
    }
    Console.WriteLine("Root cat is disposed.");
}

