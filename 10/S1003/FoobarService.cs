namespace App
{
    public class FoobarService
    {
        internal static int _latestId;
        public int Id { get; }

        private FoobarService() => Id = Interlocked.Increment(ref _latestId);
        public static FoobarService Create() => new();
    }
}