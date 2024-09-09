using TaskManager.Application.Tasks.Dtos;
using TaskManager.Domain.Core.Paging;
using TaskManager.Domain.Core.Results;
using TaskManager.Domain.Tasks;
using Task = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Application.Tasks;

public interface ITaskService
{
    Task<Result> CreateTaskAsync(Guid userId, TaskDto dto);

    Task<Result<PagedList<Task>>> GetTasksAsync(Guid userId, PagingParameters parameters,
        TaskFilter filter);

    Task<Result<Task>> GetTaskAsync(Guid userId, Guid id);

    Task<Result> UpdateTaskAsync(Guid userId, Guid taskId, TaskDto dto);

    Task<Result> DeleteTaskAsync(Guid userId, Guid taskId);
}