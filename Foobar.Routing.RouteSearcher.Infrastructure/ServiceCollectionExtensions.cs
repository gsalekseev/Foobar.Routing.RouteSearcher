using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Foobar.Routing.RouteSearcher.Application.Configuration;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo;
using Foobar.Routing.RouteSearcher.Infrastructure.Services;

namespace Foobar.Routing.RouteSearcher.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var providersSection = configuration.GetSection("ProvidersConfiguration");
            var providers = providersSection.Get<ProvidersConfiguration>();

            if (providers is null)
            {
                throw new InvalidOperationException("Cannot add providers. Check configuration files.");
            }

            serviceCollection.Configure<ProvidersConfiguration>(providersSection);
            
            foreach (var provider in providers.Providers)
            {
                serviceCollection.AddHttpClient(provider.Name,
                    client => { client.BaseAddress = new Uri(provider.Urls.BaseUrl); });
            }

            serviceCollection.AddScoped<IProviderOneHttpClient, ProviderOneHttpClient>();
            serviceCollection.AddScoped<IProviderTwoHttpClient, ProviderTwoHttpClient>();
            serviceCollection.AddScoped<IRouteProviderFactory, RouteProviderFactory>();
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

            return serviceCollection;
        }
    }
}
