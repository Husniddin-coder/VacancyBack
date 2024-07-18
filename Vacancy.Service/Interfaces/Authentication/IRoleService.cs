using Vacancy.Service.DTOs.AuthDtos.RoleDtos;

namespace Vacancy.Service.Interfaces.Authentication;

public interface IRoleService
{
    public Task<IEnumerable<RoleGetDto>> GetAllAsync();

    public Task<RoleGetDto> GetByIdAsync(int id);

    public Task<RoleGetDto> CreateAsync(RoleCreateUpdateDto dto);

    public Task<RoleGetDto> UpdateAsync(int id, RoleCreateUpdateDto dto);

    public Task<RoleGetDto> AddPermissionsIntoRoleAsync(int roleId, int giverId, int[] permissionIds);

    public Task<RoleGetDto> RemovePermissionsFromRoleAsync(int roleId, int giverId, int[] permissionIds);

    public Task<bool> DeleteAsync(int id);
}
