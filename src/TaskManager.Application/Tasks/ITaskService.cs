using TaskManager.Application.Tasks.Dtos;
using TaskManager.Domain.Core.Results;

namespace TaskManager.Application.Tasks;

public interface ITaskService
{
    Task<Result> CreateTaskAsync(Guid userId, NewTaskDto dto);
};