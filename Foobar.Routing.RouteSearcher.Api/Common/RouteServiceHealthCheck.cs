using Microsoft.Extensions.Diagnostics.HealthChecks;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Primitives;

namespace Foobar.Routing.RouteSearcher.Api.Common;

public class RouteServiceHealthCheck : IHealthCheck
{
    private readonly IRouteAggregator _aggregator;

    public static HealthStatus? status;
    public static DateTime statusExpiresIn;

    public RouteServiceHealthCheck(IRouteAggregator aggregator)
    {
        _aggregator = aggregator;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (status is null || DateTime.Now >= statusExpiresIn)
        {
            var innerStatus = await _aggregator.GetHealthStatus(cancellationToken);
            status = MapHealthCheckToHealthStatus(innerStatus);
            statusExpiresIn = DateTime.Now.AddMinutes(1); //healthcheck is actual during 1 minute
        }

        return status switch
        {
            HealthStatus.Healthy => HealthCheckResult.Healthy(),
            HealthStatus.Degraded => HealthCheckResult.Degraded(),
            _ => new HealthCheckResult(context.Registration.FailureStatus)
        };
    }

    private HealthStatus? MapHealthCheckToHealthStatus(ProviderHealthCheck healthCheck)
    {
        return healthCheck switch
        {
            ProviderHealthCheck.Degraded => HealthStatus.Degraded,
            ProviderHealthCheck.Unhealthy => HealthStatus.Unhealthy,
            ProviderHealthCheck.Healthy => HealthStatus.Healthy,
            _ => null
        };
    }
}
