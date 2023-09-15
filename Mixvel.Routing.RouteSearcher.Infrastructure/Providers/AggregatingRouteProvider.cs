using System.Threading.Tasks;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers
{
    public class AggregatingRouteProvider : IRouteProvider
    {
        public Task<RouteDto[]> Provide(RouteProviderRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsHealthy()
        {
            throw new System.NotImplementedException();
        }
    }
}
