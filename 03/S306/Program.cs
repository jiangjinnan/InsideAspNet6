using App;
using Microsoft.Extensions.DependencyInjection;

var root = new ServiceCollection()
            .AddSingleton<IFoo, Foo>()
            .AddScoped<IBar, Bar>()
            .BuildServiceProvider(true);
var child = root.CreateScope().ServiceProvider;

ResolveService<IFoo>(root);
ResolveService<IBar>(root);
ResolveService<IFoo>(child);
ResolveService<IBar>(child);

void ResolveService<T>(IServiceProvider provider)
{
    var isRootContainer = root == provider ? "Yes" : "No";
    try
    {
        provider.GetService<T>();
        Console.WriteLine($"Status: Success; Service Type: { typeof(T).Name}; Root: { isRootContainer}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Status: Fail; Service Type: { typeof(T).Name}; Root: { isRootContainer}");
        Console.WriteLine($"Error: {ex.Message}");
    }
}