using App;
using System.Diagnostics;

var random = new Random();
File.AppendAllLines("log.csv", new string[] { @"EventName,StartTime,Elapsed,ActivityId,RelatedActivityId" });
var listener = new FoobarListener();
await FooAsync();

Task FooAsync() => InvokeAsync(FoobarSource.Instance.FooStart, FoobarSource.Instance.FooStop, async () =>
{
    await BarAsync();
    await QuxAsync();
});

Task BarAsync() => InvokeAsync(FoobarSource.Instance.BarStart, FoobarSource.Instance.BarStop, BazAsync);
Task BazAsync() => InvokeAsync(FoobarSource.Instance.BazStart, FoobarSource.Instance.BazStop, () => Task.CompletedTask);
Task QuxAsync() => InvokeAsync(FoobarSource.Instance.QuxStart, FoobarSource.Instance.QuxStop, () => Task.CompletedTask);

async Task InvokeAsync(Action<long> start, Action<double> stop, Func<Task> body)
{
    start(Stopwatch.GetTimestamp());
    var sw = Stopwatch.StartNew();
    await Task.Delay(random.Next(10, 100));
    await body();
    stop(sw.ElapsedMilliseconds);
}