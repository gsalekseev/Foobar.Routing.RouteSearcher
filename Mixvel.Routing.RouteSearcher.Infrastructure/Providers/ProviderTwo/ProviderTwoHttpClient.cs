using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Primitives;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo
{
    public class ProviderTwoHttpClient : ProviderBaseClient, IProviderTwoHttpClient
    {
        private const string Name = "ProviderTwo";
        private const string GetRoutesUrl = "/";
        private const string PingUrl = "/";
        private readonly ILogger Logger;
        
        private HttpClient httpClient;
        
        public async Task<ProviderTwoRoutesResponse> GetRoutes(ProviderTwoRoutesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(GetRoutesUrl, request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ProviderTwoRoutesResponse>(cancellationToken: cancellationToken);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<ProviderHealhCheck> IsHealthy(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(PingUrl, cancellationToken);
                return this.HealthCheck(response);
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message);
                throw;
            }
        }
    }
}