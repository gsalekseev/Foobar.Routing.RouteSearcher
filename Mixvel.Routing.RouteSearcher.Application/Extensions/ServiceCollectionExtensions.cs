using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Services;
using Mixvel.Routing.RouteSearcher.Application.Common;
using Mixvel.Routing.RouteSearcher.Application.Common.Behaviours;

namespace Mixvel.Routing.RouteSearcher.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(SearchRoutesQueryHandler).GetTypeInfo().Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped<IRouteAggregator, RouteAggregator>();
            services.Configure<AppConfiguration>(configuration);
            
            services
                .AddMemoryCache()
                .AddScoped<ICacheService, CacheService>();
            
            return services;
        }
    }
}
