using System;
using System.Threading.Tasks;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces
{
    public interface IRouteProvider
    {
        Task<RouteDto[]> Provide(RouteProviderRequest request);
        
        Task<Boolean> IsHealthy();
    }
}
