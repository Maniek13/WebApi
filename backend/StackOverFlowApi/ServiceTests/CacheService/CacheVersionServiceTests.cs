using FluentAssertions;
using Infrastructure.Services.CacheServices;
using MemoryCache.Testing.Moq;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace ServiceTests.CacheService;

public class CacheVersionServiceTests
{
    [Fact]
    public void ShouldGetVersion()
    {
        var memoryCacheMock = Create.MockedMemoryCache();
        memoryCacheMock.TryGetValue("Version", out It.Ref<object>.IsAny!);
        memoryCacheMock.Set("Version", "cached-version");

        var cacheService = new CacheVersionService(
                memoryCacheMock
            );

        Action invalidate =() => cacheService.Invalidate();
        invalidate.Should().NotThrow();
    }
    [Fact]
    public void ShouldInvalidate()
    {
        var memoryCacheMock = Create.MockedMemoryCache();
        memoryCacheMock.Set("Version", "cached-version");

        var cacheService = new CacheVersionService(
                memoryCacheMock
            );

        Action invalidate = () => cacheService.Invalidate();
        invalidate.Should().NotThrow();
    }
}
