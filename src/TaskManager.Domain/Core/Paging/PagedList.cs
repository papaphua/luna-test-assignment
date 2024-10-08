﻿namespace TaskManager.Domain.Core.Paging;

public sealed class PagedList<T> : List<T>
    where T : class
{
    public PagedList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
    {
        PagedData = new PagedData
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        AddRange(items);
    }

    public PagedList()
    {
    }

    public PagedData PagedData { get; init; } = new();
}