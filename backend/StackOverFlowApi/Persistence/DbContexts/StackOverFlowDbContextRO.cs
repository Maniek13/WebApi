using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts;

public class StackOverFlowDbContextRO : DbContext
{
    public IQueryable<Tag> tags => Set<Tag>().AsNoTracking();
}
