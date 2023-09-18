using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;

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
            status = await _aggregator.GetHealthStatus(cancellationToken);
            statusExpiresIn = DateTime.Now.AddMinutes(1); //healthcheck is actual during 1 minute
        }

        if (status == HealthStatus.Healthy)
        {
            return HealthCheckResult.Healthy();
        }

        if (status == HealthStatus.Degraded)
        {
            return HealthCheckResult.Degraded();
        }

        return new HealthCheckResult(
            context.Registration.FailureStatus);
    }
}