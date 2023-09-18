namespace Mixvel.Routing.RouteSearcher.Application.Business.Routes.Interfaces;

public interface ICacheService
{
    Task SetAsync<T>(object key, T data, CancellationToken cancellationToken);

    Task<T?> GetAsync<T>(object key, CancellationToken cancellationToken);

    T? TryGetValueAsync<T>(object key);
   
    void Delete(object key, CancellationToken cancellationToken);
}
