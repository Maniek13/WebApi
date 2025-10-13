namespace Shared.Pagination;

public record PagedList<T>(int PageNumber, int PageSize, int TotalCount, List<T> Items)
{
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
}
