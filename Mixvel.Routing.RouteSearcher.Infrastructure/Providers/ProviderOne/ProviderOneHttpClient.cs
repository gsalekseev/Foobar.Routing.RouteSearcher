using Mixvel.Routing.RouteSearcher.Infrastructure.Http.Providers.ProviderOne.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Http.Providers.Shared;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne
{
    public class ProviderOneHttpClient : IProviderHttpClient<ProviderOneRoutesRequest, ProviderOneRoutesResponse>
    {
        private const string Name = "ProviderOne";
        
        private HttpClient httpClient;
        
        public ProviderOneHttpClient(IHttpClientFactory clientFactory)
        {
            this.httpClient = clientFactory.CreateClient(Name);
        }
        
        public Task<ProviderOneRoutesResponse> GetRoutes(ProviderOneRoutesRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsHealthy(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

