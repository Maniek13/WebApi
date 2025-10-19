using Abstractions.DbContexts;
using Domain.Entities.App;
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
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Entity<RefreshToken>()
            .Property(el => el.Token)
            .HasColumnName("Token");
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}