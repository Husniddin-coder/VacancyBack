using AutoMapper;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Service.DTOs.AuthDtos.UserDtos;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Interfaces.Assetss;
using Vacancy.Service.Interfaces.Authentication;

namespace Vacancy.Service.Services.Authentication;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IAssetsService _assetsService;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepo, IMapper mapper, IAssetsService assetsService, IRoleRepository roleRepo)
    {
        _userRepo = userRepo;
        _mapper = mapper;
        _assetsService = assetsService;
        _roleRepo = roleRepo;
    }

    public async Task<bool> ApproveUser(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);

        if (user is null)
            throw new VacancyException(404, "User not found");

        user.IsApproved = true;
        user = await _userRepo.UpdateAsync(user);
        return user is not null ? true : false;
    }

    public Task<User> CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _userRepo.GetByIdAsync(userId);

        if (user is null)
            throw new VacancyException(404, "User not found");

        var isDeleted = await _userRepo.DeleteAsync(userId);

        return isDeleted;
    }

    public async Task<UserGetDto> GetUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null)
            throw new VacancyException(404, "User not found");

        return _mapper.Map<UserGetDto>(user);
    }

    public async Task<IEnumerable<UserGetDto>> GetUsersAsync()
    {
        var users = await _userRepo.GetAllAsync(x => true);

        return users is null ? Enumerable.Empty<UserGetDto>() :
            _mapper.Map<IEnumerable<UserGetDto>>(users);
    }

    public async Task<UserGetDto> UpdateUserAsync(int userId, UserUpdateDto dto)
    {
        User user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            throw new VacancyException(404, "User not found");

        string oldImage = user.Image;

        user = _mapper.Map(dto, user);

        if (dto.newImage != null)
        {
            if (!string.IsNullOrEmpty(oldImage))
                await _assetsService.DeleteAsync(oldImage);

            user.Image = _assetsService.CreateAsync(dto.newImage, "Users/Images").Result.Path;
        }

        user = await _userRepo.UpdateAsync(user);

        return _mapper.Map<UserGetDto>(user);
    }

    public async Task<bool> UpdateUserRoleAsync(int userId, UserUpdateRoleDto dto)
    {
        var user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            throw new VacancyException(404, "User not found");

        var role = await _roleRepo.GetByIdAsync(dto.RoleId);

        user.Role = role;
        user.RoleId = dto.RoleId;
        user = await _userRepo.UpdateAsync(user);

        return user != null ? true : false;
    }
}
