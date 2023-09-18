using Microsoft.Extensions.Options;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Primitives;
using Mixvel.Routing.RouteSearcher.Application.Common.Helpers;
using Mixvel.Routing.RouteSearcher.Application.Configuration;

namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Services
{
    public class RouteAggregator : IRouteAggregator
    {
        private readonly IRouteProvider[] _providers;

        public RouteAggregator(IRouteProviderFactory factory, IOptions<ProvidersConfiguration> providersConfiguration)
        {
            _providers = providersConfiguration.Value.Providers.Select(p => factory.GetProvider(p.Name)).ToArray();
        }

        public async Task<RouteDto[]> AggregateFromProviders(RouteProviderRequest request, CancellationToken token)
        {
            var tasks = _providers.Select(async p =>
            {
                if (await p.IsHealthy(token))
                {
                    var result = await p.Provide(request, token);
                    return result;
                }

                return Array.Empty<RouteDto>();
            });

            var results = await Task.WhenAll(tasks);
            var routes = results
                .SelectMany(r => r)
                .Select(route => route with {Id = Md5Helper.GetMd5AsGuid(route)})
                .ToArray();

            return routes;
        }

        public async Task<ProviderHealthCheck> GetHealthStatus(CancellationToken cancellationToken)
        {
            var checks = await Task.WhenAll(_providers.Select(p => p.IsHealthy(cancellationToken)));

            if (checks.All(c => !c))
            {
                return ProviderHealthCheck.Unhealthy;
            }

            if (checks.Any(c => !c))
            {
                return ProviderHealthCheck.Degraded;
            }

            return ProviderHealthCheck.Healthy;
        }
    }
}
