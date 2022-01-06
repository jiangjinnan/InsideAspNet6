using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using System.Text;
using System.Text.Json;

var objectPool = new ServiceCollection()
            .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
            .BuildServiceProvider()
            .GetRequiredService<ObjectPoolProvider>()
            .Create(new FoobarListPolicy(1024, 1024 * 1024));

string json;
var list = objectPool.Get();
try
{
    list.AddRange(Enumerable.Range(1, 1000).Select(it => new Foobar(it, it)));
    json = JsonSerializer.Serialize(list);
}
finally
{
    objectPool.Return(list);
}