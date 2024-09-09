using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Core.Paging;
using TaskManager.Domain.Core.Paging;
using TaskManager.Domain.Tasks;
using TaskManager.Persistence.Core;
using Task = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Persistence.Tasks;

public sealed class TaskRepository(ApplicationDbContext dbContext) : Repository<Task>(dbContext), ITaskRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Task?> GetByUserIdAndIdAsync(Guid userId, Guid id)
    {
        return await _dbContext.Set<Task>()
            .FirstOrDefaultAsync(task => task.OwnerId == userId && task.Id == id);
    }
    
    public async Task<PagedList<Task>> GetAllByUserIdAsync(Guid userId, PagingParameters parameters, TaskFilter filter)
    {
        var query = _dbContext.Set<Task>()
            .Where(task => task.OwnerId == userId);

        if (filter.Status.HasValue)
            query = query.Where(task => task.Status == filter.Status);

        if (filter.DueDate.HasValue)
            query = query.Where(task => task.DueDate == filter.DueDate);

        if (filter.Priority.HasValue)
            query = query.Where(task => task.Priority == filter.Priority);

        return await query
            .OrderByDescending(task => task.UpdatedAt)
            .ToPagedListAsync(parameters);
    }
}