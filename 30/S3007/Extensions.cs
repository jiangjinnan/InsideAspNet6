using App;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IHealthChecksBuilder AddConsolePublisher(this IHealthChecksBuilder builder)
        {
            builder.Services.AddSingleton<IHealthCheckPublisher, ConsolePublisher>();
            return builder;
        }

        public static IHealthChecksBuilder ConfigurePublisher( this IHealthChecksBuilder builder, Action<HealthCheckPublisherOptions> configure)
        {
            builder.Services.Configure(configure);
            return builder;
        }

    }
}


