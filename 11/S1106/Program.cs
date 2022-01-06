using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

var cache = new ServiceCollection()
    .AddDistributedRedisCache(options =>
    {
        options.Configuration = "localhost";
        options.InstanceName = "Demo";
    })
    .BuildServiceProvider()
    .GetRequiredService<IDistributedCache>();

var time = DateTimeOffset.UtcNow.AddHours(1);
cache.SetString("Foobar", time.Ticks.ToString(), new DistributedCacheEntryOptions
{
    AbsoluteExpiration = time,
    SlidingExpiration = TimeSpan.FromMinutes(1)
});