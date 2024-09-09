using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Core.Paging;

namespace TaskManager.Application.Core.Paging;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source,
        PagingParameters parameters)
        where T : class
    {
        var totalCount = source.Count();
        var items = await source
            .Skip((parameters.CurrentPage - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedList<T>(items, totalCount, parameters.CurrentPage, parameters.PageSize);
    }
}