using App;
using Microsoft.Extensions.DependencyInjection;

using var scope = new ServiceCollection()
            .AddScoped<Fooar>()
            .BuildServiceProvider()
            .CreateScope();
scope.ServiceProvider.GetRequiredService<Fooar>();