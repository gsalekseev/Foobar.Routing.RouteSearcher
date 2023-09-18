using AutoMapper;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Models;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderOne.Mappings
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