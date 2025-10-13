using Domain.Entities.StackOverFlow;
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

        builder.Property(el => el.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.Name)
            .HasColumnName("Name");

        builder.Property(el => el.Count)
            .HasColumnName("Count");
    }
}
