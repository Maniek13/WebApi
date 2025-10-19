using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Persistence.Configurations.StackOverFlow;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");
        builder.HasKey(el => el.Id);
        builder.HasIndex(el => el.Title).IsUnique();

        builder.Property(el => el.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.Title)
            .HasColumnName("Title");

        builder.Property(el => el.Tags)
            .HasConversion(
                 v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                 v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions?)null) ?? Array.Empty<string>()
            )
            .HasColumnName("Tags");

        builder.Property(el => el.Link)
            .HasColumnName("Link");
    }
}
