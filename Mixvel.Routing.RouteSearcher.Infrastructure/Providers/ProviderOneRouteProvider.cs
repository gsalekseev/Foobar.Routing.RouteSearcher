using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers
{
    public class ProviderOneRouteProvider : IRouteProvider
    {
        private HttpClient httpClient;

        public ProviderOneRouteProvider(IHttpClientFactory clientFactory)
        {
            this.httpClient = clientFactory.CreateClient();
        }

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
