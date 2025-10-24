using Abstractions.Interfaces;
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
        var attr = typeof(TReq).GetCustomAttribute<SaveDbContextAttribute>();

        DbContext? dbContext = null;

        if (attr != null)
        {
            var contextType = attr.ContextType;
            dbContext = (DbContext)_serviceProvider.GetRequiredService(contextType)!;
        }

        var response = await next();
        
        if(dbContext != null)
            await dbContext.SaveChangesAsync(cancellationToken);

        return response;
    }
}
