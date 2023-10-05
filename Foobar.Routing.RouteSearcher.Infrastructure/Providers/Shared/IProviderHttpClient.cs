using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Primitives;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.Shared
{
    public interface IProviderHttpClient<in TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> GetRoutes(TRequest request,
            CancellationToken cancellationToken);

        Task<ProviderHealthCheck> IsHealthy(CancellationToken cancellationToken);
    }
}
