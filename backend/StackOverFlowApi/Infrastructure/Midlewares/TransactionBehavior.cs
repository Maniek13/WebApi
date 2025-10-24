using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Atributes;
using System.Reflection;

namespace Infrastructure.Middleware;

public class TransactionBehavior<TReq, TRes> : IPipelineBehavior<TReq, TRes>
    where TReq : notnull
{
    private readonly IServiceProvider _serviceProvider;
    public TransactionBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
    {
        var attr = typeof(TReq).GetCustomAttribute<DbContextAtribute>();

       
        if (attr != null)
        {
            var contextType = attr.ContextType;
            var dbContext = (DbContext)_serviceProvider.GetRequiredService(contextType)!;

            using var transaction = dbContext.Database.BeginTransaction();

            try
            {
                var response = await next();

                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        else
        {
            return await next();
        }
    }
}
