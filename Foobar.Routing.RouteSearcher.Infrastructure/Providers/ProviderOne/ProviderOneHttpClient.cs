using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Primitives;
using Foobar.Routing.RouteSearcher.Application.Configuration;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.Helpers;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Models;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.Shared;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne
{
    public class ProviderOneHttpClient: ProviderBaseClient,IProviderOneHttpClient{

        private const string Name = "ProviderOne";
        private readonly ILogger<ProviderOneHttpClient> _logger;
        private readonly ProviderUrls _urls;
        
        private HttpClient httpClient;
        
        public ProviderOneHttpClient(IHttpClientFactory clientFactory, ILogger<ProviderOneHttpClient> logger, IOptions<ProvidersConfiguration> providersConfiguration)
        {
            _logger = logger;
            this.httpClient = clientFactory.CreateClient(Name);

            this._urls = ProvidersOptionsHelper.GetUrlsFromConfiguration(Name, providersConfiguration);
        }
        
        public async Task<ProviderOneRoutesResponse> GetRoutes(ProviderOneRoutesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(_urls.SearchRoutesUrl, request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ProviderOneRoutesResponse>(cancellationToken: cancellationToken);
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
                HttpResponseMessage response = await httpClient.GetAsync(_urls.HealthCheckUrl, cancellationToken);
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

