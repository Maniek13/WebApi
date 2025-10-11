namespace Abstractions.Interfaces;

public interface IRepositoryROBase<TEntity>
{
    public Task<bool> CheckHaveData(CancellationToken ct);
}
