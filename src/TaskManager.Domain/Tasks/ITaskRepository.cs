using TaskManager.Domain.Core;
using TaskManager.Domain.Core.Paging;

namespace TaskManager.Domain.Tasks;

public interface ITaskRepository : IRepository<Task>
{
    Task<Task?> GetByUserIdAndIdAsync(Guid userId, Guid id);
    
    Task<PagedList<Task>> GetAllByUserIdAsync(Guid userId, PagingParameters parameters, TaskFilter filter);
}