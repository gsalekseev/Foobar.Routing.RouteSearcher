using System.Net;
using System.Net.Http;
using System.Xml;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Primitives;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.Shared
{
    public abstract class ProviderBaseClient
    { 
        public ProviderHealhCheck HealthCheck(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return ProviderHealhCheck.Healthy;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return ProviderHealhCheck.Unhealthy;
            }
                
            return ProviderHealhCheck.Unknown;
        }
    }
}