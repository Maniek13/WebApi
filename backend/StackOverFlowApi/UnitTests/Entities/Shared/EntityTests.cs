using Domain.Entities.StackOverFlow;
using FluentAssertions;

namespace UnitTests.Entities.Shared;

public class EntityTests
{
    [Fact] 
    public async Task ShouldCheckHavePropertyByNameAndReturnTrue()
    {
        string name = "name";
        long count = 1;

        var tag = Tag.Create(name, count);

        Tag.CheckHavePropertyByName<Tag>("Id").Should().BeTrue();
        Tag.CheckHavePropertyByName<Tag>("Name").Should().BeTrue();
        Tag.CheckHavePropertyByName<Tag>("Count").Should().BeTrue();
    }

    [Fact]
    public async Task ShouldCheckHavePropertyByNameAndReturnFalse()
    {
        string name = "name";
        long count = 1;

        var tag = Tag.Create(name, count);

        Tag.CheckHavePropertyByName<Tag>("NotHavedProperty").Should().BeFalse();
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
