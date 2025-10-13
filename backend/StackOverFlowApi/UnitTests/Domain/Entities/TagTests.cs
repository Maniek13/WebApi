using Domain.Entities.StackOverFlow;
using FluentAssertions;

namespace UnitTests.Domain.Entities;

public class TagTests
{
    [Fact]
    public async Task ShouldCreateTag()
    {
        string name = "name";
        long count = 1;

        var tag = Tag.Create(name, count);

        tag.Name.Should().Be(name);
        tag.Count.Should().Be(count);
    }

    [Fact]
    public async Task ShouldEqualsOther()
    {
        string name = "name";
        long count = 1;

        var tag = Tag.Create(name, count);

        tag.Equals(tag).Should().BeTrue();
    }
}
