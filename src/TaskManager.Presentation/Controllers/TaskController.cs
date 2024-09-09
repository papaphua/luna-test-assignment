using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Tasks;
using TaskManager.Application.Tasks.Dtos;
using TaskManager.Domain.Core.Paging;
using TaskManager.Domain.Tasks;
using TaskManager.Presentation.Core;

namespace TaskManager.Presentation.Controllers;

[Route("api/tasks")]
public sealed class TaskController(ITaskService taskService) : ApiController
{
    [HttpPost]
    public async Task<IResult> CreateTask([FromForm] NewTaskDto dto)
    {
        var result = await taskService.CreateTaskAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpGet]
    public async Task<IResult> GetTasks([FromQuery] PagingParameters parameters, [FromQuery] TaskFilter filter)
    {
        var result = await taskService.GetTasksAsync(UserId, parameters, filter);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}