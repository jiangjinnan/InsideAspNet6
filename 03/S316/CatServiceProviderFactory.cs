using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class CatServiceProviderFactory : IServiceProviderFactory<CatBuilder>
    {
        public CatBuilder CreateBuilder(IServiceCollection services)
        {
            var cat = new Cat();
            foreach (var service in services)
            {
                if (service.ImplementationFactory != null)
                {
                    cat.Register(service.ServiceType, provider => service.ImplementationFactory(provider), service.Lifetime.AsCatLifetime());
                }
                else if (service.ImplementationInstance != null)
                {
                    cat.Register(service.ServiceType, service.ImplementationInstance);
                }
                else
                {
                    cat.Register(service.ServiceType, service.ImplementationType ?? throw new InvalidOperationException("Invalid service registry."), service.Lifetime.AsCatLifetime());
                }
            }
            return new CatBuilder(cat);
        }
        public IServiceProvider CreateServiceProvider(CatBuilder containerBuilder) => containerBuilder.BuildServiceProvider();
    }

}
