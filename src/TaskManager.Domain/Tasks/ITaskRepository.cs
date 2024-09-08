using TaskManager.Domain.Core;
using TaskManager.Domain.Core.Paging;

namespace TaskManager.Domain.Tasks;

public interface ITaskRepository : IRepository<Task>
{
    Task<PagedList<Task>> GetAllByUserId(Guid userId, PagingParameters parameters, TaskFilter filter);
}