using System;
using AutoMapper;
using AutoMapper.Execution;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Models;
using Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo.Models;

namespace Mixvel.Routing.RouteSearcher.Infrastructure.Providers.ProviderTwo.Mappings
{
    public class ProviderTwoMappingProfile : Profile
    {
        public ProviderTwoMappingProfile()
        {
            CreateMap<RouteProviderRequest, ProviderTwoRoutesRequest>()
                .ForMember(dest => dest.Arrival,
                    expression => expression.MapFrom(src => src.To))
                .ForMember(dest => dest.Departure,
                    expression => expression.MapFrom(src => src.From))
                .ForMember(dest => dest.DepartureDate,
                    expression => expression.MapFrom(src => src.DateFrom));

            CreateMap<ProviderTwoRoute, RouteDto>()
                .ForMember(dest => dest.Destination,
                    exp => exp.MapFrom(src => src.Arrival != null ? src.Arrival.Point : string.Empty))
                .ForMember(dest => dest.Origin,
                    exp => exp.MapFrom(src => src.Departure != null ? src.Departure.Point : string.Empty))
                .ForMember(dest => dest.TimeLimit,
                    exp => exp.MapFrom(src => DateTime.Now.AddDays(7))) //one week time limit
                .ForMember(dest => dest.DestinationDateTime,
                    exp => exp.MapFrom(src => src.Departure != null ? src.Departure.Date : DateTime.MinValue))
                .ForMember(dest => dest.OriginDateTime,
                    exp => exp.MapFrom(src => src.Departure != null ? src.Departure.Date : DateTime.MinValue));
        }
    }
}
