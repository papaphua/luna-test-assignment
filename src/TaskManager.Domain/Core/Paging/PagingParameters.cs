namespace TaskManager.Domain.Core.Paging;

public sealed class PagingParameters(int currentPage, int pageSize)
{
    public int PageSize { get; set; } = pageSize;

    public int CurrentPage { get; set; } = currentPage;
}