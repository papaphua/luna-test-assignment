namespace TaskManager.Domain.Core.Paging;

public sealed class PagingParameters
{
    public int PageSize { get; set; }

    public int CurrentPage { get; set; }
}