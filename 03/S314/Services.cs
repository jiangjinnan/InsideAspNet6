namespace App
{
    public class Foo { }
    public class Bar { }
    public class Baz { }

    public class Foobar
    {
        public Foobar(Foo foo) => Console.WriteLine("Foobar(Foo foo)");
        public Foobar(Foo foo, Bar bar) => Console.WriteLine("Foobar(Foo foo, Bar bar)");
    }
    public class BarBaz
    {
        public BarBaz(Bar bar, Baz baz) => Console.WriteLine("BarBaz(Bar bar, Baz baz)");
        public BarBaz(Bar bar) => Console.WriteLine("BarBaz(Bar bar)");
    }

}