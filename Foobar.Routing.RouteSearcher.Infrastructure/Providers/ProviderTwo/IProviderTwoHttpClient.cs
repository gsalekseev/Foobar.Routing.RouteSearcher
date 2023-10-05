using Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo.Models;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo
{
    public interface IProviderTwoHttpClient : IProviderHttpClient<ProviderTwoRoutesRequest, ProviderTwoRoutesResponse>
    {
        
    }
}