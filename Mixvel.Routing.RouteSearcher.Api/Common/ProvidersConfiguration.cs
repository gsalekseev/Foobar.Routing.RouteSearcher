
namespace Mixvel.Routing.RouteSearcher.Api.Common
{
    public class ProvidersConfiguration
    {
        public ProviderConfiguration[] Providers { get; set; }
    
        public class ProviderConfiguration
        {
            public string Name { get; set; }
    
            public string BaseUrl { get; set; }

            public string HealthCheckUrl { get; set; } 
        }
    }
}
