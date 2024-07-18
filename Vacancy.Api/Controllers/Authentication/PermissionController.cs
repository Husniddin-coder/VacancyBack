using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Attributes;
using Vacancy.Api.Controllers.BaseControllers;
using Vacancy.Domain.Enums.Users;
using Vacancy.Service.DTOs.AuthDtos.PermissionDtos;
using Vacancy.Service.Interfaces.Authentication;

namespace Vacancy.Api.Controllers.Authentication;

public class PermissionController : BaseController
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
        => _permissionService = permissionService;

    [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewPermissions)]
    public async Task<IActionResult> GetAllPermissions()
        => Ok(await _permissionService.GetAllAsync());

    [HttpGet("[action]/{id}"), PermissionAuthorize(UserPermission.ViewPermission)]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _permissionService.GetByIdAsync(id));

    [HttpPut("[action]/{id}"), ValidateModel, PermissionAuthorize(UserPermission.UpdatePermission)]
    public async Task<IActionResult> UpdatePermissionName([FromQuery] int id, PermissionUpdateDto userDto)
        => Ok(await _permissionService.UpdatePermissionNameAsync(userDto, id));
}
