using Domain.Entities.StackOverFlow;
using Domain.Entities.StackOverFlow.EntityIds;
using Domain.Entities.StackOverFlow.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Persistence.Configurations.StackOverFlow;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");
        builder.HasKey(el => el.Id);
        builder.HasIndex(el => el.QuestionNumber).IsUnique();

        builder.Property(el => el.Id)
            .HasConversion(
                el => el.Id,
                value => new QuestionId(value)
            )
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.QuestionNumber)
            .HasConversion(
                el => el.Value,
                value => new QuestionNumber(value!)
            )
            .HasColumnName("QuestionId");

        builder.Property(el => el.UserNumber)
            .HasConversion(
                el => el.Value,
                value => new UserNumber(value)
            )
            .HasColumnName("UserNumber")
            .IsRequired(false);

        builder.Property(el => el.Title)
            .HasColumnName("Title");

        builder.Property(el => el.Tags)
            .HasColumnName("Tags")
            .HasConversion(
                 v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                 v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions?)null) ?? Array.Empty<string>()
            )
            .Metadata.SetValueComparer(
                new ValueComparer<string[]>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToArray()
            ));

        builder.Property(el => el.Link)
            .HasColumnName("Link");
    }
}
