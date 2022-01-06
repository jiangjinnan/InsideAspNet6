using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

var objectPool = new ServiceCollection()
    .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
    .BuildServiceProvider()
    .GetRequiredService<ObjectPoolProvider>()
    .Create(new FoobarPolicy());


while (true)
{
    Console.Write("Used services: ");
    await Task.WhenAll(Enumerable.Range(1, 3).Select(_ => ExecuteAsync()));
    Console.Write("\n");
}
async Task ExecuteAsync()
{
    var service = objectPool.Get();
    try
    {
        Console.Write($"{service.Id}; ");
        await Task.Delay(1000);
    }
    finally
    {
        objectPool.Return(service);
    }
}
