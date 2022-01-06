namespace App
{
    public class Foo { }
    public class Bar { }
    public class Foobar
    {
        public string Name { get; }
        public Foo Foo { get; }
        public Bar Bar { get; }

        public Foobar(string name, Foo foo, Bar bar)
        {
            Name = name;
            Foo = foo;
            Bar = bar;
        }
    }
}