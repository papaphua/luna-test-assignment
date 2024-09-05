using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Users;
using TaskManager.Application.Users.Dtos;
using TaskManager.Presentation.Core;

namespace TaskManager.Presentation.Controllers;

[Route("api/users")]
public sealed class UserController(IUserService userService) : ApiController
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IResult> Register(RegisterDto dto)
    {
        var result = await userService.RegisterAsync(dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IResult> Login(LoginDto dto)
    {
        var result = await userService.LoginAsync(dto);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}