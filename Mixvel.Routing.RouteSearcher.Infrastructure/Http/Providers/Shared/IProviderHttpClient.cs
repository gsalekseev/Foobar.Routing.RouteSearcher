using System.Threading;
using System.Threading.Tasks;
using Mixvel.Routing.RouteSearcher.Infrastructure.Http.Providers.ProviderOne.Models;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Http.Providers.Shared
{
    public interface IProviderHttpClient<in TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> GetRoutes(TRequest request,
            CancellationToken cancellationToken);

        Task<bool> IsHealthy(CancellationToken cancellationToken);
    }
}
