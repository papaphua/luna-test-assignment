using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Tasks;
using TaskManager.Application.Tasks.Dtos;
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
}