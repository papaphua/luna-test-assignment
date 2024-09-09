using TaskManager.Application.Tasks.Dtos;
using TaskManager.Domain.Core.Paging;
using TaskManager.Domain.Core.Results;
using TaskManager.Domain.Tasks;
using Task = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Application.Tasks;

public interface ITaskService
{
    Task<Result> CreateTaskAsync(Guid userId, NewTaskDto dto);

    Task<Result<PagedList<Task>>> GetTasksAsync(Guid userId, PagingParameters parameters,
        TaskFilter filter);
};