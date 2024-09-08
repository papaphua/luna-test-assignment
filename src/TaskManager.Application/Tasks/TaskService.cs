using TaskManager.Application.Core;
using TaskManager.Application.Tasks.Dtos;
using TaskManager.Domain.Core.Results;
using TaskManager.Domain.Tasks;
using TaskManager.Domain.Users;
using Task = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Application.Tasks;

public sealed class TaskService(
    ITaskRepository taskRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ITaskService
{
    public async Task<Result> CreateTaskAsync(Guid userId, NewTaskDto dto)
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
};