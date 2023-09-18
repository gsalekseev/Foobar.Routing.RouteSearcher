using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Primitives;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Models;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne
{
    public class ProviderOneRouteService : IRouteProvider
    {
        private readonly IProviderOneHttpClient _httpClient;
        private readonly IMapper mapper;

        public ProviderOneRouteService(IProviderOneHttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            this.mapper = mapper;
        }

        public async Task<RouteDto[]> Provide(RouteProviderRequest request, CancellationToken cancellationToken)
        {
            var innerRequest = mapper.Map<ProviderOneRoutesRequest>(request);
            var routes = await _httpClient.GetRoutes(innerRequest, cancellationToken);

            return mapper.Map<RouteDto[]>(routes.Routes);
        }

        public async Task<bool> IsHealthy(CancellationToken cancellationToken)
        {
            var status = await _httpClient.IsHealthy(cancellationToken);
            return status == ProviderHealthCheck.Healthy;
        }
    }
}