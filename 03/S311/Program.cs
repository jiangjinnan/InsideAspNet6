using App;
using Microsoft.Extensions.DependencyInjection;

await using var scope = new ServiceCollection()
    .AddScoped<Fooar>()
    .BuildServiceProvider()
    .CreateAsyncScope();
scope.ServiceProvider.GetRequiredService<Fooar>();

