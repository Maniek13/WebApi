using Domain.Entities.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abstractions.DbContexts;

public abstract class AbstractAppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AbstractAppDbContext(DbContextOptions options)
        : base(options)
    {

    }
}
