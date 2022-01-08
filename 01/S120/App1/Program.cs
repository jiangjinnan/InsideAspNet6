using Dapr.Actors;
using Dapr.Actors.Client;
using Shared;

var accumulator1 = ActorProxy.Create<IAccumulator>(new ActorId("001"), "Accumulator");
var accumulator2 = ActorProxy.Create<IAccumulator>(new ActorId("002"), "Accumulator");

while (true)
{
    var counter1 = await accumulator1.IncreaseAsync(1);
    var counter2 = await accumulator2.IncreaseAsync(2);
    await Task.Delay(5000);
    Console.WriteLine($"001: {counter1}");
    Console.WriteLine($"002: {counter2}\n");

    if (counter1 > 10)
    {
        await accumulator1.ResetAsync();
    }
    if (counter2 > 20)
    {
        await accumulator2.ResetAsync();
    }
}