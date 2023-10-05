using Microsoft.Extensions.Options;
using Foobar.Routing.RouteSearcher.Application.Configuration;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.Helpers;

public static class ProvidersOptionsHelper
{
    public static ProviderUrls GetUrlsFromConfiguration(string providerName, IOptions<ProvidersConfiguration> configuration)
    {
        var providerConfiguration = configuration.Value.Providers.FirstOrDefault(p => string.Equals(p.Name, providerName, StringComparison.CurrentCultureIgnoreCase));

        if (providerConfiguration is null)
        {
            throw new InvalidOperationException($"No configuration found for provider {providerName}");
        }

        return providerConfiguration.Urls;
    }
}
