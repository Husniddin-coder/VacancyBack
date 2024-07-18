using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Filters;
using Vacancy.Domain.Enums.Users;

namespace Vacancy.Api.Attributes;

public class PermissionAuthorizeAttribute : TypeFilterAttribute
{
    public PermissionAuthorizeAttribute(params UserPermission[] permissions) : base(typeof(PermissionFilter))
    {
        this.Arguments = new object[] { permissions.Cast<int>().ToArray() };
    }
}
