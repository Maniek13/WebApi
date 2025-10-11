namespace Abstractions.Repositories;

public interface ITagsRepository
{
    public Task SetTags(CancellationToken ct);
    public Task RefreshTags(CancellationToken ct);
}
