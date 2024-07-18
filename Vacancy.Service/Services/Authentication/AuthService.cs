using AutoMapper;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Service.DTOs.AuthDtos.AccountDtos;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Extensions;
using Vacancy.Service.Interfaces.Authentication;
using Vacancy.Service.Interfaces.Authentication.Tokens;

namespace Vacancy.Service.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly ITokenRepository _tokenRepo;
    private readonly IUserRepository _userRepo;
    private readonly IJWTokenService _jwtService;
    private readonly IRoleRepository _roleRepo;
    private readonly IMapper _mapper;

    public AuthService(IMapper mapper,IRoleRepository roleRepo, IJWTokenService jwtService, IUserRepository userRepo, ITokenRepository tokenRepo)
    {
        _mapper = mapper;
        _roleRepo = roleRepo;
        _jwtService = jwtService;
        _userRepo = userRepo;
        _tokenRepo = tokenRepo;
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = await _userRepo.GetByIdAsync(dto.UserId);

        if (user == null)
            throw new VacancyException(404, "User not found");

        if (user.Password != dto.OldPassword.GetHash())
            throw new VacancyException(400, "Incorrect Password");

        if (dto.NewPassword != dto.ConfirmPasswrd)
            throw new VacancyException(400, "New and Confirmation passwords should match");

        user.Password = dto.NewPassword.GetHash();

        return await _userRepo.UpdateAsync(user) is not null ? true : false;
    }

    public async Task<bool> DeleteTokensAsync(AccessAndRefreshTokens dto)
    {
        var tokens = await _tokenRepo
            .GetAllAsync(x => x.AccessToken == dto.AccessToken && x.RefreshToken == dto.RefreshToken);

        if (tokens.FirstOrDefault() == null)
            throw new VacancyException(404, "Tokens not found");

        return await _tokenRepo.DeleteAsync(tokens.FirstOrDefault().Id);
    }

    public async Task<AccessAndRefreshTokensWithUser> RefreshTokensAsync(AccessAndRefreshTokens dto)
    {
        var token = await _tokenRepo
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.AccessToken == dto.AccessToken &&
                                 x.RefreshToken == dto.RefreshToken);

        if (token == null)
            throw new VacancyException(404, "Tokens not found");

        if (token.ExpiredDateOfRefreshToken < DateTime.UtcNow)
        {
            await _tokenRepo.DeleteAsync(token.Id);
            throw new VacancyException(400, "Refresh token already expired");
        }

        ClaimsPrincipal principal = await _jwtService.GetClaimsPrincipalAsync(dto.AccessToken);
        var userId = principal.Claims.FirstOrDefault(x => x.Type == CustomClaimNames.UserId);

        var user = await _userRepo.GetByIdAsync(int.Parse(userId.Value));
        var newToken = await _jwtService.GenerateAccessAndRefreshTokensAsync(user, token.RememberMe);

        token.AccessToken = newToken.AccessToken;
        token.RefreshToken = newToken.RefreshToken;
        token.ExpiredDateOfRefreshToken = newToken.ExpiredDate;

        token = await _tokenRepo.UpdateAsync(token);
        token.User = user;
        newToken.User = _mapper.Map<UserResponse>(user);

        return newToken;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userRepo
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.Email == registerDto.Email);

        if (existingUser is not null)
            throw new VacancyException(400, "User with this email already exists");

        var roleUser = await _roleRepo.GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.isDefault);

        var newUser = new User()
        {
            UserName = registerDto.UserName,
            Password = registerDto.Password.GetHash(),
            Email = registerDto.Email,
            Role = roleUser,
            RoleId = roleUser.Id
        };

        var roleEmployer = await _roleRepo.GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.Name == "Employer");

        if (registerDto.IsEmployer)
        {
            newUser.IsApproved = false;
            newUser.Company = registerDto.Company;
            newUser.Region = registerDto.Region;
            newUser.Role = roleEmployer;
            newUser.RoleId = roleEmployer.Id;
        }

        return _userRepo.CreateAsync(newUser) == null ? false : true;
    }

    public async Task<AccessAndRefreshTokensWithUser> SignInAsync(SignInDto signInDto)
    {
        var user = await _userRepo
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.Email == signInDto.Email && x.Password == signInDto.Password.GetHash());

        if (user == null)
            throw new VacancyException(404, "Email or Password wrong !");

        var tokens = await _jwtService.GenerateAccessAndRefreshTokensAsync(user, false);
        tokens.User = _mapper.Map<UserResponse>(user);

        var NewToken = new TokenModel()
        {
            UserId = user.Id,
            User = user,
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            ExpiredDateOfRefreshToken = tokens.ExpiredDate,
        };

        NewToken = await _tokenRepo.CreateAsync(NewToken);

        return tokens;
    }
}
