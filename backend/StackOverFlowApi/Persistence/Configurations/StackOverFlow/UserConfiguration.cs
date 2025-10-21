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
        builder.HasIndex(el => el.AccountId).IsUnique();

        builder.Property(el => el.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(el => el.AccountId)
            .HasColumnName("AccountId");

        builder.Property(el => el.DispalaName)
            .HasColumnName("DispalaName")
            .IsRequired(false);

        builder.Property(el => el.CreatedAt)
            .HasColumnName("CreatedAt");
    }
}
