using App;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
            .AddTransient<IFoo, Foo>()
            .AddScoped<IBar>(_ => new Bar())
            .AddSingleton<IBaz>(new Baz());

var factory = new CatServiceProviderFactory();
var builder = factory.CreateBuilder(services)
    .Register(typeof(Foo).Assembly);
var container = factory.CreateServiceProvider(builder);

GetServices();
GetServices();
Console.WriteLine("\nRoot container is disposed.");
(container as IDisposable)?.Dispose();

void GetServices()
{
    using var scope = container.CreateScope();
    Console.WriteLine("\nService scope is created.");
    var child = scope.ServiceProvider;

    child.GetService<IFoo>();
    child.GetService<IBar>();
    child.GetService<IBaz>();
    child.GetService<IQux>();

    child.GetService<IFoo>();
    child.GetService<IBar>();
    child.GetService<IBaz>();
    child.GetService<IQux>();
    Console.WriteLine("\nService scope is disposed.");
}

