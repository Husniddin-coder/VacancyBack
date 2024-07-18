using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vacancy.Api.Attributes;
using Vacancy.Service.Interfaces.Authentication.Tokens;

namespace Vacancy.Api.Controllers.BaseControllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public abstract class BaseController : ControllerBase
{
    private readonly int _id;
    public virtual int UserId
    {
        get
        {
            var rawUserId = this.User.FindFirstValue(CustomClaimNames.UserId);
            return int.TryParse(rawUserId, out int _id) ? _id : default(int);
        }
    }
}
