namespace Contracts.Requests;

public record GetTagsRequest(
        int Page = 1,
        int PageSize = 100,
        string SortBy = "Id",
        bool Descanding = false
    );
