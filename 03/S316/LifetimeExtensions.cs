using Microsoft.Extensions.DependencyInjection;

namespace App
{
    internal static class Extensions
    {
        public static Lifetime AsCatLifetime(this ServiceLifetime lifetime)
        {
            return lifetime switch
            {
                ServiceLifetime.Scoped => Lifetime.Self,
                ServiceLifetime.Singleton => Lifetime.Root,
                _ => Lifetime.Transient,
            };
        }
    }

}
