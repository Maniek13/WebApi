using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.Interfaces;
using Contracts.Dtos;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Cache;
using Infrastructure.Services;
using MapsterMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json.Linq;
using MemoryCache.Testing.Moq;

namespace ServiceTests.Services;

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
