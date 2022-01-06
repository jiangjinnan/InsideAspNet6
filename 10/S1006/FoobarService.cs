namespace App
{
    public class FoobarService : IDisposable
    {
        internal static int _latestId;
        public int Id { get; }
        private FoobarService() => Id = Interlocked.Increment(ref _latestId);
        public static FoobarService Create() => new FoobarService();
        public void Dispose() => Console.Write($"{Id}; ");
    }

}