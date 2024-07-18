using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Interfaces.Authentication.Tokens;

namespace Vacancy.Api.Filters;

public class PermissionFilter : IAsyncAuthorizationFilter
{
    private readonly int[] _requiredPermissionCodes;

    public PermissionFilter(int[] requiredPermissionCodes)
    {
        _requiredPermissionCodes = requiredPermissionCodes;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var rawRoleId = context.HttpContext.User.FindFirstValue(CustomClaimNames.Role);

        if (!rawRoleId.IsNullOrEmpty() && int.TryParse(rawRoleId, out int roleId) && roleId == 0)
        {
            await Task.CompletedTask;
            return;
        }

        var rawPermissionCodes = context.HttpContext.User.FindFirstValue(CustomClaimNames.Permissions);
        if (rawPermissionCodes.IsNullOrEmpty())
            throw new VacancyException(401, "UnAuthorized");

        var permissionCodes = rawPermissionCodes.Split(", ").Select(int.Parse);

        var anyNotMatched = _requiredPermissionCodes.Any(x => permissionCodes.All(p => p != x));

        if (anyNotMatched)
            throw new VacancyException(403, "Forbidden");
    }
}
