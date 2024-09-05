using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Presentation.Core;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            var stringId = HttpContext.User.FindFirstValue("nameid");
            return Guid.TryParse(stringId, out var guid)
                ? guid
                : Guid.Empty;
        }
    }
}