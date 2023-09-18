using AutoMapper;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Common;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Services
{
    public class RouteProviderFactory : IRouteProviderFactory
    {
        private readonly IProviderOneHttpClient _providerOneHttpClient;
        private readonly IProviderTwoHttpClient _providerTwoHttpClient;
        private readonly IMapper _mapper;

        public RouteProviderFactory(IProviderOneHttpClient providerOneHttpClient, IProviderTwoHttpClient providerTwoHttpClient, IMapper mapper)
        {
            _providerOneHttpClient = providerOneHttpClient;
            _providerTwoHttpClient = providerTwoHttpClient;
            _mapper = mapper;
        }

        public IRouteProvider GetProvider(string name)
        {
            if (name == AppConstants.ProviderNames.ProviderOneName)
            {
                return new ProviderOneRouteService(_providerOneHttpClient, _mapper);
            }
            
            if (name == AppConstants.ProviderNames.ProviderTwoName)
            {
                return new ProviderTwoRouteService(_providerTwoHttpClient, _mapper);
            }

            throw new InvalidOperationException($"Unknown Route provider {name}");
        }
    }
}
