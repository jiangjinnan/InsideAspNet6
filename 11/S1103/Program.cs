using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

var cache = new ServiceCollection()
   .AddDistributedSqlServerCache(options =>
   {
       options.ConnectionString ="server=.;database=demodb;uid=sa;pwd=password";
        options.SchemaName = "dbo";
        options.TableName = "AspnetCache";
   })
    .BuildServiceProvider()
    .GetRequiredService<IDistributedCache>();

for (int index = 0; index < 5; index++)
{
    Console.WriteLine(await GetCurrentTimeAsync());
    await Task.Delay(1000);
}

async Task<DateTimeOffset> GetCurrentTimeAsync()
{
    var timeLiteral = await cache.GetStringAsync("CurrentTime");
    if (string.IsNullOrEmpty(timeLiteral))
    {
        await cache.SetStringAsync("CurrentTime", timeLiteral = DateTimeOffset.UtcNow.ToString());
    }
    return DateTimeOffset.Parse(timeLiteral);
}
