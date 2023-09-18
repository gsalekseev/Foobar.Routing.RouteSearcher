using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo
{
    public interface IProviderTwoHttpClient : IProviderHttpClient<ProviderTwoRoutesRequest, ProviderTwoRoutesResponse>
    {
        
    }
}