namespace App
{
    public class FoobarService
    {
        internal static int _latestId;
        public int Id { get; }
        public FoobarService() => Id = Interlocked.Increment(ref _latestId);
    }
}