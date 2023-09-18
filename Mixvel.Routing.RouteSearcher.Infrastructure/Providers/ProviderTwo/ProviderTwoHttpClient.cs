using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Primitives;
using Mixvel.Routing.RouteSearcher.Application.Configuration;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.Helpers;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo
{
    public class ProviderTwoHttpClient : ProviderBaseClient, IProviderTwoHttpClient
    {
        private const string Name = "ProviderTwo";
        private readonly ILogger<ProviderTwoHttpClient> _logger;
        private readonly HttpClient _httpClient;
        private ProviderUrls _urls;

        public ProviderTwoHttpClient(ILogger<ProviderTwoHttpClient> logger, HttpClient httpClient, IOptions<ProvidersConfiguration> configuration)
        {
            _logger = logger;
            this._httpClient = httpClient;
            this._urls = ProvidersOptionsHelper.GetUrlsFromConfiguration(Name, configuration);
        }

        public async Task<ProviderTwoRoutesResponse> GetRoutes(ProviderTwoRoutesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_urls.SearchRoutesUrl, request, cancellationToken);

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
                this._logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<ProviderHealthCheck> IsHealthy(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_urls.HealthCheckUrl, cancellationToken);
                return this.HealthCheck(response);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }
    }
}
