using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Service.DTOs.AuthDtos.PermissionDtos;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Interfaces.Authentication;

namespace Vacancy.Service.Services.Authentication;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepo;
    private readonly IMapper _mapper;

    public PermissionService(IPermissionRepository permissionRepo, IMapper mapper)
    {
        _permissionRepo = permissionRepo;
        _mapper = mapper;
    }


    public async Task<IEnumerable<PermissionGetDto>> GetAllAsync()
    {
        var permissions = await _permissionRepo.GetAllAsync(x => true);

        var permissionsGetDto = _mapper.Map<IEnumerable<PermissionGetDto>>(permissions);

        return permissions.IsNullOrEmpty() ? Enumerable.Empty<PermissionGetDto>() :
            _mapper.Map<IEnumerable<PermissionGetDto>>(permissions);
    }

    public async Task<PermissionGetDto> GetByIdAsync(int id)
    {
        var permission = await _permissionRepo.GetByIdAsync(id);

        if (permission == null)
            throw new VacancyException(404, $"Permission with id: {id} not found");

        return _mapper.Map<PermissionGetDto>(permission);
    }

    public async Task<PermissionGetDto> UpdatePermissionNameAsync(PermissionUpdateDto dto, int id)
    {
        var existingPermission = await _permissionRepo.GetByIdAsync(id);

        if (existingPermission == null)
            throw new VacancyException(404, $"Permission with id: {id} not found");

        existingPermission.Name = dto.Name;

        return _mapper.Map<PermissionGetDto>(existingPermission);
    }
}
