using Abstractions.DbContexts;
using Domain.Entities.App;
using Domain.Entities.App.EntityIds;
using Domain.Entities.App.ValueObjects;
using Domain.Entities.StackOverFlow.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts.App;

public class AppDbContext : AbstractAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        #region user

        builder.Entity<ApplicationUser>(b =>
        {
            b.OwnsOne(el => el.UserAddress, uab =>
            {
                uab.Property(el => el.City)
                    .HasColumnName("City")
                    .IsRequired(false);

                uab.Property(el => el.Street)
                    .HasColumnName("Street")
                    .IsRequired(false);
            });

            b.OwnsMany(el => el.Messages, mb =>
            {
                mb.WithOwner()
                    .HasForeignKey("UserId");

                mb.Property(el => el.Message)
                    .HasColumnName("Message");

                mb.ToTable("Messages");
            });
        });



        #endregion

        #region RefreshToke

        builder.Entity<RefreshToken>()
            .ToTable("RefreshToken");
        builder.Entity<RefreshToken>()
            .HasKey(el => el.Id);

        builder.Entity<RefreshToken>()
            .HasOne(el => el.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RefreshToken>()
            .Property(el => el.Id)
            .HasConversion(
                el => el.Id,
                value => new RefreshTokenId(value)
            )
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Entity<RefreshToken>()
            .Property(el => el.Token)
            .HasColumnName("Token");
        
        #endregion
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}