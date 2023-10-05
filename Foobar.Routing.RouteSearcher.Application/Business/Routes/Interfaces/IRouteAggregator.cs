using Foobar.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Models;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Primitives;

namespace Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces
{
    public interface IRouteAggregator
    {
        Task<RouteDto[]> AggregateFromProviders(RouteProviderRequest request, CancellationToken token);
        
        Task<ProviderHealthCheck> GetHealthStatus (CancellationToken cancellationToken);
    }
}
