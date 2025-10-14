using Domain.Entities.StackOverFlow;
using FluentAssertions;

namespace UnitTests.Entities.Domain;

public class TagTests
{

    [Fact]
    public async Task ShouldCreateTag()
    {
        string name = "name";
        long count = 1;
        double parcipitation = 1;

        var tag = Tag.Create(name, count, parcipitation);

        tag.Name.Should().Be(name);
        tag.Count.Should().Be(count);
    }

    [Fact]
    public async Task ShouldEqualsOther()
    {
        string name = "name";
        long count = 1;
        double parcipitation = 1;

        var tag = Tag.Create(name, count, parcipitation);

        tag.Equals(tag).Should().BeTrue();
    }
}
