using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo;
using Mixvel.Routing.RouteSearcher.Infrastructure.Services;

namespace Mixvel.Routing.RouteSearcher.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProviderOneHttpClient, ProviderOneHttpClient>();
            serviceCollection.AddScoped<IProviderTwoHttpClient, ProviderTwoHttpClient>();
            serviceCollection.AddScoped<IRouteProviderFactory, RouteProviderFactory>();
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            return serviceCollection;
        }
    }
}