using System.Diagnostics.Tracing;

namespace App
{
    [EventSource(Name = "Foobar")]
    public sealed class FoobarSource : EventSource
    {
        public static readonly FoobarSource Instance = new();

        [Event(1)]
        public void FooStart(long timestamp) => WriteEvent(1, timestamp);
        [Event(2)]
        public void FooStop(double elapsed) => WriteEvent(2, elapsed);

        [Event(3)]
        public void BarStart(long timestamp) => WriteEvent(3, timestamp);
        [Event(4)]
        public void BarStop(double elapsed) => WriteEvent(4, elapsed);

        [Event(5)]
        public void BazStart(long timestamp) => WriteEvent(5, timestamp);
        [Event(6)]
        public void BazStop(double elapsed) => WriteEvent(6, elapsed);

        [Event(7)]
        public void QuxStart(long timestamp) => WriteEvent(7, timestamp);
        [Event(8)]
        public void QuxStop(double elapsed) => WriteEvent(8, elapsed);
    }

}