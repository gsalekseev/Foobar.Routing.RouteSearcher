using Mixvel.Routing.RouteSearcher.Api.Common;

namespace Mixvel.Routing.RouteSearcher.Api.Extensions.ServiceCollection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRouteProviders(this IServiceCollection services, IConfiguration configuration)
    {
        var providersSection = configuration.GetSection("ProvidersConfiguration");
        var providers = providersSection.Get<ProvidersConfiguration>();
        
        if (providers is null)
        {
            throw new InvalidOperationException("Cannot add providers. Check configuration files.");
        }
        
        services.Configure<ProvidersConfiguration>(providersSection);
        services.Configure<AppConfiguration>(configuration);

        foreach (var provider in providers.Providers)
        {
            services.AddHttpClient(provider.Name,client =>
            {
                client.BaseAddress = new Uri(provider.BaseUrl);
            });
        }
        return services;
    }
}
