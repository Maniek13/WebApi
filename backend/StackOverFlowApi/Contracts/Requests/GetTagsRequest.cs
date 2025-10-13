namespace Contracts.Requests;


public record GetTagsRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public string SortBy { get; set; } = "Id";
    public bool Descanding { get; set; } = false;
}
