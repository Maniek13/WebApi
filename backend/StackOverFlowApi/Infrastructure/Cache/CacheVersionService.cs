using Abstractions.Caches;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache;

public class CacheVersionService : ICacheVersionService
{
    private readonly IMemoryCache _cache;
    private const string _versionKey = "Version";

    public CacheVersionService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string GetVersion()
    {
        if(!_cache.TryGetValue(_versionKey, out string version))
        {
            version = Guid.NewGuid().ToString();
            _cache.Set(_versionKey, version);
        }

        return version;
    }

    public void Invalidate() => _cache.Set(_versionKey, Guid.NewGuid().ToString());
}
