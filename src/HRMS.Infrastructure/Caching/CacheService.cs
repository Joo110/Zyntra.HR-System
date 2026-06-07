using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using HRMS.Application.Common.Interfaces;
namespace HRMS.Infrastructure.Caching;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    public CacheService(IDistributedCache cache) { _cache = cache; }
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var data = await _cache.GetStringAsync(key, cancellationToken);
        return data == null ? null : JsonSerializer.Deserialize<T>(data);
    }
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class
    {
        var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(10) };
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken);
    }
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default) => await _cache.RemoveAsync(key, cancellationToken);
    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default) => await _cache.GetAsync(key, cancellationToken) != null;
}
