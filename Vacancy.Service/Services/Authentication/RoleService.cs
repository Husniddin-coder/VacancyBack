using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Service.DTOs.AuthDtos.RoleDtos;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Interfaces.Authentication;

namespace Vacancy.Service.Services.Authentication;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepo;
    private readonly IUserRepository _userRepo;
    private readonly IPermissionRepository _permissionRepo;
    private readonly IRolePermissionRepository _role_permissionsRepo;
    private readonly IMapper _mapper;

    public RoleService(IMapper mapper, IRolePermissionRepository role_permissionsRepo, IPermissionRepository permissionRepo, IUserRepository userRepo, IRoleRepository roleRepo)
    {
        _mapper = mapper;
        _role_permissionsRepo = role_permissionsRepo;
        _permissionRepo = permissionRepo;
        _userRepo = userRepo;
        _roleRepo = roleRepo;
    }

    public async Task<RoleGetDto> AddPermissionsIntoRoleAsync(int roleId, int giverId, int[] permissionIds)
    {
        var role = await _roleRepo.GetByIdAsync(roleId);

        if (role == null)
            throw new VacancyException(404, $"Role with id: {roleId} not found");

        var user = await _userRepo.GetByIdAsync(giverId);

        if (user == null)
            throw new VacancyException(404, $"User with id: {user.Id} not found");

        foreach (var permissionId in permissionIds)
        {
            var existingPermission = await _permissionRepo.GetByIdAsync(permissionId);
            if (existingPermission == null)
                throw new VacancyException(404, $"Permission with id: {roleId} not found");
        }
        List<RolePermission> newRolePermissions = new List<RolePermission>();

        role.RolePermissions = null;
        await _roleRepo.UpdateAsync(role);

        foreach (var permissionId in permissionIds)
        {
            newRolePermissions.Add(new RolePermission
            {
                RoleId = roleId,
                Role = role,
                GivenById = giverId,
                GivenBy = user,
                PermissionId = permissionId,
                Permission = await _permissionRepo.GetByIdAsync(permissionId)
            });
        }

        var result = _role_permissionsRepo.AddRange(newRolePermissions);

        return result == true ? _mapper.Map<RoleGetDto>(role) :
            throw new VacancyException(400, "Adding permissions failed");
    }

    public async Task<RoleGetDto> CreateAsync(RoleCreateUpdateDto dto)
    {
        Role role = _mapper.Map<Role>(dto);

        var roleGetDto = _mapper.Map<RoleGetDto>(await _roleRepo.CreateAsync(role));

        return roleGetDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await _roleRepo.GetByIdAsync(id);

        if (role == null)
            throw new VacancyException(404, "Role not found");

        var isDeleted = await _roleRepo.DeleteAsync(id);

        return isDeleted;
    }

    public async Task<IEnumerable<RoleGetDto>> GetAllAsync()
    {
        var roles = await _roleRepo.GetAllAsync(x => true);

        return roles.IsNullOrEmpty() ? Enumerable.Empty<RoleGetDto>() :
            _mapper.Map<IEnumerable<RoleGetDto>>(roles);
    }

    public async Task<RoleGetDto> GetByIdAsync(int id)
    {
        var role = await _roleRepo.GetByIdAsync(id);

        if (role == null)
            throw new VacancyException(404, $"Role with id: {id} not found");

        return _mapper.Map<RoleGetDto>(role);
    }

    public async Task<RoleGetDto> RemovePermissionsFromRoleAsync(int roleId, int giverId, int[] permissionIds)
    {
        var role = await _roleRepo.GetByIdAsync(roleId);

        if (role == null)
            throw new VacancyException(404, $"Role with id: {roleId} not found");

        var user = await _userRepo.GetByIdAsync(giverId);

        if (user == null)
            throw new VacancyException(404, $"User with id: {roleId} not found");

        foreach (var permissionId in permissionIds)
        {
            var existingPermission = _permissionRepo.GetByIdAsync(permissionId);
            if (existingPermission == null)
                throw new VacancyException(404, $"Permission with id: {roleId} not found");
        }

        var rolePermissions = await _role_permissionsRepo
            .GetAllAsync(x => x.RoleId == roleId && permissionIds
            .Any(p => p == x.PermissionId));

        var result = _role_permissionsRepo.RemoveRange(rolePermissions);
            

        return result == true ? _mapper.Map<RoleGetDto>(role) :
            throw new VacancyException(400, "Removing permissions failed");
    }

    public async Task<RoleGetDto> UpdateAsync(int id, RoleCreateUpdateDto dto)
    {
        var existingRole = await _roleRepo.GetByIdAsync(id);

        if (existingRole == null)
            throw new VacancyException(404, $"Role with id: {id} not found");

        existingRole.Name = dto.Name;
        existingRole.isDefault = dto.isDefault;

        var roleGetDto = _mapper.Map<RoleGetDto>(existingRole);

        return roleGetDto;
    }
}
