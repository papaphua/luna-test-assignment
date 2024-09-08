namespace TaskManager.Domain.Core.Paging;

public sealed class PagedData
{
    public int CurrentPage { get; init; }

    public int TotalPages { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;
}