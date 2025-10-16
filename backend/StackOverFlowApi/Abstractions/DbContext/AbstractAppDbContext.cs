using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abstractions.DbContext;

public abstract class AbstractAppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public AbstractAppDbContext(DbContextOptions options)
        : base(options)
    {

    }
}
