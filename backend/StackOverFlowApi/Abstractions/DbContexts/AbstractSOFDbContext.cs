using Microsoft.EntityFrameworkCore;

namespace Abstractions.DbContexts;

public abstract class AbstractSOFDbContext : DbContext
{
    public AbstractSOFDbContext(DbContextOptions options)
        : base(options)
    {

    }
}
