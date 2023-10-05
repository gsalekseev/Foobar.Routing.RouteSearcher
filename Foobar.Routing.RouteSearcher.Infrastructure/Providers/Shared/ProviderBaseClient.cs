using System.Net;
using System.Net.Http;
using System.Xml;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Primitives;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.Shared
{
    public abstract class ProviderBaseClient
    { 
        public ProviderHealthCheck HealthCheck(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return ProviderHealthCheck.Healthy;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return ProviderHealthCheck.Unhealthy;
            }
                
            return ProviderHealthCheck.Unknown;
        }
    }
}