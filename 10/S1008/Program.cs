using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

var objectPool = new ServiceCollection()
    .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
    .BuildServiceProvider()
    .GetRequiredService<ObjectPoolProvider>()
    .CreateStringBuilderPool(1024, 1024 * 1024);

var builder = objectPool.Get();
try
{
    for (int index = 0; index < 100; index++)
    {
        builder.Append(index);
    }
    Console.WriteLine(builder);
}
finally
{
    objectPool.Return(builder);
}
