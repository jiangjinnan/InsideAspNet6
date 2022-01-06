using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

var cache = new ServiceCollection()
    .AddMemoryCache()
    .BuildServiceProvider()
    .GetRequiredService<IMemoryCache>();

for (int index = 0; index < 5; index++)
{
    Console.WriteLine(GetCurrentTime());
    await Task.Delay(1000);
}

DateTimeOffset GetCurrentTime()
{
    if (!cache.TryGetValue<DateTimeOffset>("CurrentTime", out var currentTime))
    {
        cache.Set("CurrentTime", currentTime = DateTimeOffset.UtcNow);
    }
    return currentTime;
}