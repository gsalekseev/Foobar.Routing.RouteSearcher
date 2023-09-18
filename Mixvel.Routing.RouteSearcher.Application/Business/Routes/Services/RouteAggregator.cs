using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mixvel.Routing.RouteSearcher.Api.Common;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Services
{
    public class RouteAggregator : IRouteAggregator
    {
        private readonly IRouteProviderFactory _factory;
        private readonly IRouteProvider[] _providers;
        private readonly ProvidersConfiguration _providersConfiguration;

        public RouteAggregator(IRouteProviderFactory factory, ProvidersConfiguration providersConfiguration, IRouteProvider providers)
        {
            _factory = factory;
            _providersConfiguration = providersConfiguration;
            _providers = providersConfiguration.Providers.Select(p => factory.GetProvider(p.Name)).ToArray();
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
            var routes = results.SelectMany(r => r).ToArray();


            return routes;
        }

        public async Task<HealthStatus> GetHealthStatus(CancellationToken cancellationToken)
        {
            var checks = await Task.WhenAll(_providers.Select(p => p.IsHealthy(cancellationToken)));

            if (checks.All(c => !c))
            {
                return HealthStatus.Unhealthy;
            }

            if (checks.Any(c => !c))
            {
                return HealthStatus.Degraded;
            }

            return HealthStatus.Healthy;
        }
    }
}