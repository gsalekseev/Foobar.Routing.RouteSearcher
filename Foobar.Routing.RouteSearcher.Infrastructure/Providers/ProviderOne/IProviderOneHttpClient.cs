using Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Models;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne
{
    public interface IProviderOneHttpClient: IProviderHttpClient<ProviderOneRoutesRequest, ProviderOneRoutesResponse>

    {
        
    }
}