using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Controllers.BaseControllers;
using Vacancy.Service.DTOs.AuthDtos.AccountDtos;
using Vacancy.Service.Interfaces.Authentication;
using Vacancy.Service.Interfaces.Authentication.Tokens;
using Vacancy.Service.Services.Authentication;

namespace Vacancy.Api.Controllers.Authentication;

public class AccountController : BaseController
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
        => Ok(await _authService.RegisterAsync(registerDto));

    [HttpPost("[action]")]
    public async Task<IActionResult> SignIn(SignInDto signInDto)
       => Ok(await _authService.SignInAsync(signInDto));


    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken(AccessAndRefreshTokens token)
        => Ok(await _authService.RefreshTokensAsync(token));

    [HttpPost("[action]")]
    public async Task<IActionResult> LogOut(AccessAndRefreshTokens token)
        => Ok(await _authService.DeleteTokensAsync(token));

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        => Ok(await _authService.ChangePasswordAsync(changePasswordDto));
}
