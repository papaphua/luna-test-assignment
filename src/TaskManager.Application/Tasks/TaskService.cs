using TaskManager.Application.Core;
using TaskManager.Application.Tasks.Dtos;
using TaskManager.Domain.Core.Paging;
using TaskManager.Domain.Core.Results;
using TaskManager.Domain.Tasks;
using Task = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Application.Tasks;

public sealed class TaskService(
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork)
    : ITaskService
{
    public async Task<Result> CreateTaskAsync(Guid userId, TaskDto dto)
    {
        var task = new Task
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Status = dto.Status,
            Priority = dto.Priority,
            OwnerId = userId
        };

        try
        {
            await taskRepository.AddAsync(task);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(TaskErrors.InternalError);
        }

        return Result.Success();
    }

    public async Task<Result<PagedList<Task>>> GetTasksAsync(Guid userId, PagingParameters parameters,
        TaskFilter filter)
    {
        var tasks = await taskRepository.GetAllByUserIdAsync(userId, parameters, filter);

        return Result<PagedList<Task>>.Success(tasks);
    }

    public async Task<Result<Task>> GetTaskAsync(Guid userId, Guid id)
    {
        var task = await taskRepository.GetByUserIdAndIdAsync(userId, id);

        if (task == null) return Result<Task>.Failure(TaskErrors.TaskNotFound);

        return Result<Task>.Success(task);
    }

    public async Task<Result> UpdateTaskAsync(Guid userId, Guid taskId, TaskDto dto)
    {
        var task = await taskRepository.GetByUserIdAndIdAsync(userId, taskId);

        if (task == null) return Result.Failure(TaskErrors.TaskNotFound);

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;
        task.Status = dto.Status;
        task.Priority = dto.Priority;

        try
        {
            taskRepository.Update(task);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(TaskErrors.InternalError);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await taskRepository.GetByUserIdAndIdAsync(userId, taskId);

        if (task == null) return Result.Failure(TaskErrors.TaskNotFound);

        try
        {
            taskRepository.Remove(task);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(TaskErrors.InternalError);
        }

        return Result.Success();
    }
}