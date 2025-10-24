using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.StackOverFlow;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(el => el.Id);
        builder.HasIndex(el => el.UserId).IsUnique();

        builder
            .HasMany(el => el.Questions)
            .WithOne(el => el.User)
            .HasForeignKey(el => el.UserId)
            .HasPrincipalKey(el => el.UserId)
            .IsRequired(false);

        builder.Property(el => el.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.UserId)
            .HasColumnName("UserId");

        builder.Property(el => el.DisplayName)
            .HasColumnName("DisplayName")
            .IsRequired(false);

        builder.Property(el => el.CreatedAt)
            .HasColumnName("CreatedAt");
    }
}
