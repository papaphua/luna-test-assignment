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
    public async Task<IResult> CreateTask([FromForm] TaskDto dto)
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

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetTask([FromRoute] Guid id)
    {
        var result = await taskService.GetTaskAsync(UserId, id);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> UpdateTask([FromRoute] Guid id, [FromForm] TaskDto dto)
    {
        var result = await taskService.UpdateTaskAsync(UserId, id, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeleteTask([FromRoute] Guid id)
    {
        var result = await taskService.DeleteTaskAsync(UserId, id);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}