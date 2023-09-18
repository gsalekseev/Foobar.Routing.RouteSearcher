using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;

namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes
{
    public class SearchRoutesQueryHandler : IRequestHandler<SearchRoutesQuery, RouteDto[]>
    {
        private readonly IRouteAggregator _aggregator;
        private readonly IMapper _mapper;

        public SearchRoutesQueryHandler(IRouteAggregator aggregator, IMapper mapper)
        {
            _aggregator = aggregator;
            _mapper = mapper;
        }

        public async Task<RouteDto[]> Handle(SearchRoutesQuery request, CancellationToken cancellationToken)
        {
            var innerRequest = _mapper.Map<RouteProviderRequest>(request);

            return await _aggregator.AggregateFromProviders(innerRequest, cancellationToken);
        }
    }
}