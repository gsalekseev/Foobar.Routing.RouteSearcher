using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Services;
using Foobar.Routing.RouteSearcher.Application.Common;
using Foobar.Routing.RouteSearcher.Application.Common.Behaviours;

namespace Foobar.Routing.RouteSearcher.Application.Extensions
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
