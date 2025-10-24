namespace Abstractions.Repositories;

public interface IRepositoryROBase<TEntity>
{
    public Task<bool> CheckHaveData(CancellationToken ct);
    public IQueryable<TEntity> GetAll();
}
