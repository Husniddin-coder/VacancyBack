using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Attributes;
using Vacancy.Api.Controllers.BaseControllers;
using Vacancy.Domain.Enums.Users;
using Vacancy.Service.DTOs.AuthDtos.UserDtos;
using Vacancy.Service.Interfaces.Authentication;

namespace Vacancy.Api.Controllers.Authentication
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
            => _userService = userService;

        [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewUsers)]
        public async Task<IActionResult> GetAllUsers()
            => Ok(await _userService.GetUsersAsync());

        [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewUser)]
        public async Task<IActionResult> GetUser()
            => Ok(await _userService.GetUserAsync(this.UserId));

        [HttpPut("[action]"), PermissionAuthorize(UserPermission.UpdateUser)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto dto)
            => Ok(await _userService.UpdateUserAsync(this.UserId, dto));

        [HttpPut("[action]/{id}"), PermissionAuthorize(UserPermission.UpdateUserRole)]
        public async Task<IActionResult> UpdateUserRole(int id,UserUpdateRoleDto dto)
            => Ok(await _userService.UpdateUserRoleAsync(id, dto));

        [HttpDelete("[action]"), PermissionAuthorize(UserPermission.DeleteUser)]
        public async Task<IActionResult> DeleteUser()
            => Ok(await _userService.DeleteUserAsync(this.UserId));

        [HttpPost("[action]"), PermissionAuthorize(UserPermission.ApproveUser)]
        public async Task<IActionResult> ApproveUser(int id)
            => Ok(await _userService.ApproveUser(id));
    }
}
