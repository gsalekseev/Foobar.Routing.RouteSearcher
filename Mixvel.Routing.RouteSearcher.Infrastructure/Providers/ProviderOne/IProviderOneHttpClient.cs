using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne
{
    public interface IProviderOneHttpClient: IProviderHttpClient<ProviderOneRoutesRequest, ProviderOneRoutesResponse>

    {
        
    }
}