using System;
using System.Threading;
using System.Threading.Tasks;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces
{
    public interface IRouteProvider
    {
        Task<RouteDto[]> Provide(RouteProviderRequest request, CancellationToken cancellationToken);
        
        Task<Boolean> IsHealthy(CancellationToken cancellationToken);
    }
}
