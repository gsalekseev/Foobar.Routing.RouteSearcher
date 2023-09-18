using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Primitives;

namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces
{
    public interface IRouteAggregator
    {
        Task<RouteDto[]> AggregateFromProviders(RouteProviderRequest request, CancellationToken token);
        
        Task<ProviderHealthCheck> GetHealthStatus (CancellationToken cancellationToken);
    }
}
