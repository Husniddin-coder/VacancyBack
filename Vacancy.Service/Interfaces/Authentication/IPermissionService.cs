using Vacancy.Service.DTOs.AuthDtos.PermissionDtos;

namespace Vacancy.Service.Interfaces.Authentication;

public interface IPermissionService
{
    public Task<IEnumerable<PermissionGetDto>> GetAllAsync();

    public Task<PermissionGetDto> GetByIdAsync(int id);

    public Task<PermissionGetDto> UpdatePermissionNameAsync(PermissionUpdateDto dto, int id);
}
