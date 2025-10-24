using Abstractions.Interfaces;
using MediatR;

namespace Infrastructure.Middleware;

public class TransactionBehavior<TReq, TRes> : IPipelineBehavior<TReq, TRes>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
    {
        using var transaction = _unitOfWork.Context.Database.BeginTransaction();

        try
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
