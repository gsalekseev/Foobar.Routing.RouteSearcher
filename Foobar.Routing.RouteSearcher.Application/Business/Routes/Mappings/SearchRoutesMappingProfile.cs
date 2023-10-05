using AutoMapper;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Models;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes;

namespace Foobar.Routing.RouteSearcher.Application.Business.Routes.Mappings;

public class SearchRoutesMappingProfile : Profile
{
    public SearchRoutesMappingProfile()
    {
        CreateMap<SearchRoutesQuery, RouteProviderRequest>()
            .ForMember(dest => dest.From, expression => expression.MapFrom(src => src.Origin))
            .ForMember(dest => dest.To, expression => expression.MapFrom(src => src.Destination))
            .ForMember(dest => dest.DateFrom, expression => expression.MapFrom(src => src.OriginDateTime))
            .ForMember(dest => dest.DateTo,
                expression => expression.MapFrom(src => src.Filters != null ? src.Filters.DestinationDateTime : null))
            .ForMember(dest => dest.MaxPrice,
                expression => expression.MapFrom(src => src.Filters != null ? src.Filters.MaxPrice : null))
            .ForMember(dest => dest.MinTimeLimit,
                expression => expression.MapFrom(src => src.Filters != null ? src.Filters.MinTimeLimit : null));
        
    }
}
