namespace App
{
    public class FoobarHttpMessageHandler : DelegatingHandler
    {
        public FoobarHttpMessageHandler(Foo foo, Bar bar)
            => Console.WriteLine($"[{DateTimeOffset.Now}]{GetType().Name} is constructed.");
        protected override void Dispose(bool disposing)
        {
            Console.WriteLine($"[{DateTimeOffset.Now}]{GetType().Name} is Disposed.");
            base.Dispose(disposing);
        }
    }

    public abstract class DependentService : IDisposable
    {
        protected DependentService()
            => Console.WriteLine($"[{DateTimeOffset.Now}]{GetType().Name} is constructed.");
        public void Dispose()
            => Console.WriteLine($"[{DateTimeOffset.Now}]{GetType().Name} is disposed.");
    }
    public class Foo : DependentService { }
    public class Bar : DependentService { }
}