using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Service.DTOs.AuthDtos.AccountDtos;
using Vacancy.Service.Interfaces.Authentication.Tokens;

namespace Vacancy.Service.Interfaces.Authentication;

public interface IAuthService
{
    public Task<bool> RegisterAsync(RegisterDto registerDto);

    public Task<AccessAndRefreshTokensWithUser> SignInAsync(SignInDto signInDto);

    public Task<bool> DeleteTokensAsync(AccessAndRefreshTokens dto);

    public Task<AccessAndRefreshTokensWithUser> RefreshTokensAsync(AccessAndRefreshTokens dto);

    public Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
}
