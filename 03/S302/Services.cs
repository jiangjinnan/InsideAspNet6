namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IFoobar<T1, T2> { }
    public class Base : IDisposable
    {
        public Base()=> Console.WriteLine($"An instance of {GetType().Name} is created.");
        public void Dispose() => Console.WriteLine($"The instance of {GetType().Name} is disposed.");
    }

    public class Foo : Base, IFoo, IDisposable { }
    public class Bar : Base, IBar, IDisposable { }
    public class Baz : Base, IBaz, IDisposable { }
    public class Foobar<T1, T2> : IFoobar<T1, T2>
    {
        public T1 Foo { get; }
        public T2 Bar { get; }
        public Foobar(T1 foo, T2 bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }
}
