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
        var response = await next(); 
        await _unitOfWork.SaveChangesAsync(cancellationToken); 
        return response;
    }
}
