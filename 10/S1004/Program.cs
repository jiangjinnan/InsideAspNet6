using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

var objectPool = new ServiceCollection()
            .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
            .BuildServiceProvider()
            .GetRequiredService<ObjectPoolProvider>()
            .Create(new FoobarPolicy());
var poolSize = Environment.ProcessorCount * 2;
while (true)
{
    while (true)
    {
        await Task.WhenAll(Enumerable.Range(1, poolSize)
            .Select(_ => ExecuteAsync()));
        Console.WriteLine($"Last service: {FoobarService._latestId}");
    }
}

async Task ExecuteAsync()
{
    var service = objectPool.Get();
    try
    {
        await Task.Delay(1000);
    }
    finally
    {
        objectPool.Return(service);
    }
}
