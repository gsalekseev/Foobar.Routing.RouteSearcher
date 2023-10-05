using System.Data;
using Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces;
using Microsoft.Extensions.Caching.Memory;
namespace Foobar.Routing.RouteSearcher.Application.Business.Routes.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task SetAsync<T>(object key, T data, CancellationToken cancellationToken)
    {
        _cache.Remove(key);
        await _cache.GetOrCreateAsync(key, _ => Task.Factory.StartNew(() => data, cancellationToken));
    }

    public async Task<T?> GetAsync<T>(object key, CancellationToken cancellationToken)
    {
        var result = await _cache.GetOrCreateAsync(key,
            _ => Task.Factory.StartNew<T>(() => throw new DataException("Requested data not found"),
                cancellationToken));
        return result;
    }

    public T? TryGetValueAsync<T>(object key)
    {
        return _cache.TryGetValue(key, out T? data) ? data : default;
    }

    public void Delete(object key, CancellationToken cancellationToken)
    {
        _cache.Remove(key);
    }
}
