using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Service.DTOs.AuthDtos.UserDtos;

namespace Vacancy.Service.Interfaces.Authentication;

public interface IUserService
{
    public Task<IEnumerable<UserGetDto>> GetUsersAsync();

    public Task<UserGetDto> GetUserAsync(int id);

    public Task<bool> ApproveUser(int id);

    public Task<User> CreateUser(User user);

    public Task<UserGetDto> UpdateUserAsync(int userId, UserUpdateDto dto);

    public Task<bool> UpdateUserRoleAsync(int userId, UserUpdateRoleDto dto);

    public Task<bool> DeleteUserAsync(int userId);
}
