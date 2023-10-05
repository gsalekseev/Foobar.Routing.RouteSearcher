using AutoMapper;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Models;
using Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Models;

namespace Foobar.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Mappings
{
    public class ProviderOneMappingProfile : Profile
    {
        public ProviderOneMappingProfile()
        {
            CreateMap<RouteProviderRequest, ProviderOneRoutesRequest>();
            CreateMap<ProviderOneRoute, RouteDto>()
                .ForMember(dest => dest.Destination,
                    expression => expression.MapFrom(src => src.To))
                .ForMember(dest => dest.Origin,
                    expression => expression.MapFrom(src => src.From))
                .ForMember(dest => dest.DestinationDateTime,
                    expression => expression.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.OriginDateTime,
                    expression => expression.MapFrom(src => src.DateFrom));
        }
    }
}