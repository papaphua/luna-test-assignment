using TaskManager.Domain.Core;
using TaskManager.Domain.Core.Paging;

namespace TaskManager.Domain.Tasks;

public interface ITaskRepository : IRepository<Task>
{
    Task<PagedList<Task>> GetAllByUserIdAsync(Guid userId, PagingParameters parameters, TaskFilter filter);
}