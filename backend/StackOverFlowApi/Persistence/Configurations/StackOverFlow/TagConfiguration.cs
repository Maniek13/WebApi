using Domain.Entities.StackOverFlow;
using Domain.Entities.StackOverFlow.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.StackOverFlow;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        builder.HasKey(el => el.Id);
        builder.HasIndex(el => el.Name).IsUnique();

        builder.Ignore(el => el.Questions);

        builder.Property(el => el.Id)
             .HasConversion(
                el => el.Id,
                value => new TagId(value)
            )
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.Name)
            .HasColumnName("Name");

        builder.Property(el => el.Count)
            .HasColumnName("Count");

        builder.Property(el => el.Participation)
            .HasColumnName("Participation");
    }
}
