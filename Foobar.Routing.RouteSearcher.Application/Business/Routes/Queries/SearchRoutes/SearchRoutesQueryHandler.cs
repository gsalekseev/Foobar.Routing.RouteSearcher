using AutoMapper;
using MediatR;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Foobar.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes
{
    public class SearchRoutesQueryHandler : IRequestHandler<SearchRoutesQuery, SearchRoutesResultDto>
    {
        private readonly IRouteAggregator _aggregator;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string RoutesCacheKey = "RoutesList";

        public SearchRoutesQueryHandler(IRouteAggregator aggregator, IMapper mapper, ICacheService cacheService)
        {
            _aggregator = aggregator;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<SearchRoutesResultDto> Handle(SearchRoutesQuery request, CancellationToken cancellationToken)
        {
            var routesRequest = _mapper.Map<RouteProviderRequest>(request);
            var cacheRequired = request.Filters?.OnlyCached != null && request.Filters.OnlyCached.Value;
            
            RouteDto[] routes; 
            
            if (cacheRequired)
            {
                //TODO: make a more specific key to reduce filtering
                var cachedRoutes = await _cacheService.GetAsync<RouteDto[]>(RoutesCacheKey, cancellationToken) ?? Array.Empty<RouteDto>();
                routes = FilterCachedRoutes(routesRequest, cachedRoutes);
            }
            else
            {
                var providersRoutes = await _aggregator.AggregateFromProviders(routesRequest, cancellationToken);
                await _cacheService.SetAsync(RoutesCacheKey, providersRoutes, cancellationToken);

                routes = providersRoutes;
            }

            var durations = routes.Select(route => Math.Round((route.DestinationDateTime - route.OriginDateTime).TotalMinutes)).ToArray();
            var prices = routes.Select(route => route.Price).ToArray();
            
            var maxPrice = prices.Max();
            var minPrice = prices.Min();
            var shortest = Convert.ToInt32(durations.Min());
            var longest = Convert.ToInt32(durations.Max());

            return new SearchRoutesResultDto()
            {
                Routes = routes,
                MaxMinutesRoute = longest,
                MinMinutesRoute = shortest,
                MaxPrice = maxPrice,
                MinPrice = minPrice
            };
        }

        private RouteDto[] FilterCachedRoutes(RouteProviderRequest request, RouteDto[] array)
        {
            return array
                .Where(r => string.Equals(r.Destination, request.To, StringComparison.CurrentCultureIgnoreCase))
                .Where(r => string.Equals(r.Origin, request.From, StringComparison.CurrentCultureIgnoreCase))
                .Where(r => r.Price <= request.MaxPrice)
                .Where(r => r.DestinationDateTime == request.DateTo)
                .Where(r => r.OriginDateTime == request.DateFrom)
                .Where(r => request.MinTimeLimit >= r.TimeLimit)
                .ToArray();
        }
    }
}
