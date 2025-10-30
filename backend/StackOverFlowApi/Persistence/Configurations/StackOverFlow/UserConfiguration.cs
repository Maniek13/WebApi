using Domain.Entities.StackOverFlow;
using Domain.Entities.StackOverFlow.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.StackOverFlow;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(el => el.Id);
        builder.HasIndex(el => el.UserNumber).IsUnique();

        builder
            .HasMany(el => el.Questions)
            .WithOne(el => el.User)
            .HasForeignKey(el => el.UserNumber)
            .HasPrincipalKey(el => el.UserNumber)
            .IsRequired(false);

        builder.Property(el => el.Id)
            .HasConversion(
                el => el.Id,
                value => new UserId(value)
            )
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.UserNumber)
            .HasConversion(
                el => el.Value,
                value => new UserNumber(value!)
            )
            .HasColumnName("UserId");

        builder.Property(el => el.DisplayName)
            .HasColumnName("DisplayName")
            .IsRequired(false);

        builder.Property(el => el.CreatedAt)
            .HasColumnName("CreatedAt");
    }
}
