using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Attributes;
using Vacancy.Api.Controllers.BaseControllers;
using Vacancy.Domain.Enums.Users;
using Vacancy.Service.DTOs.AuthDtos.RoleDtos;
using Vacancy.Service.Interfaces.Authentication;

namespace Vacancy.Api.Controllers.Authentication
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
            => _roleService = roleService;

        [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewRoles)]
        public async Task<IActionResult> GetAllRoles()
            => Ok(await _roleService.GetAllAsync());

        [HttpGet("[action]/{id}"), PermissionAuthorize(UserPermission.ViewRole)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _roleService.GetByIdAsync(id));

        [HttpDelete("[action]/{id}"), PermissionAuthorize(UserPermission.DeleteRole)]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _roleService.DeleteAsync(id));

        [HttpPost("[action]"), ValidateModel, PermissionAuthorize(UserPermission.AddRole)]
        public async Task<IActionResult> Add(RoleCreateUpdateDto userDto)
            => Ok(await _roleService.CreateAsync(userDto));

        [HttpPut("[action]/{id}"), ValidateModel, PermissionAuthorize(UserPermission.UpdateRole)]
        public async Task<IActionResult> Update(int id, RoleCreateUpdateDto userDto)
        => Ok(await _roleService.UpdateAsync(id, userDto));

        [HttpPut("[action]"), ValidateModel, PermissionAuthorize(UserPermission.AddingPermissionsIntoRole)]
        public async Task<IActionResult> AddPermissionsIntoRole(AssignPermissionsIntoRoleDto dto)
            => Ok(await _roleService.AddPermissionsIntoRoleAsync(dto.RoleId, this.UserId, dto.PermissionIds));

        [HttpPut("[action]"), ValidateModel, PermissionAuthorize(UserPermission.RemovingPermissionFromRole)]
        public async Task<IActionResult> RemovePermissionsFromRole(AssignPermissionsIntoRoleDto dto)
            => Ok(await _roleService.RemovePermissionsFromRoleAsync(dto.RoleId, this.UserId, dto.PermissionIds));
    }
}
