using System;
using MediatR;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;

namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes
{
    
    public class SearchRoutesQuery: IRequest<SearchRoutesResultDto>
    {
        // Mandatory
        // Start point of route, e.g. Moscow 
        public string Origin { get; set; }
    
        // Mandatory
        // End point of route, e.g. Sochi
        public string Destination { get; set; }
    
        // Mandatory
        // Start date of route
        public DateTime OriginDateTime { get; set; }
        
        // Optional
        public SearchFiltersDto? Filters { get; set; }
    }
}
