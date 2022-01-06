namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo
    {
        public IBar Bar { get; }
        public Foo(IBar bar) => Bar = bar;
    }
    public class Bar : IBar { }
}