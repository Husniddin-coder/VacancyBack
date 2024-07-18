using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Service.DTOs.AuthDtos.JWTOptions;
using Vacancy.Service.Extensions;
using Vacancy.Service.Interfaces.Authentication.Tokens;

namespace Vacancy.Service.Services.Authentication.Tokens;

public class JWTokenService : IJWTokenService
{
    private readonly JWTOption _jwtOptions;

    public JWTokenService(IOptions<JWTOption> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    private async Task<JwtSecurityToken> CreateTokenAsync(IEnumerable<Claim> claims, bool rememberMe)
    {
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audiance,
            expires: rememberMe == false ? DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifeTime) : DateTime.UtcNow.AddDays(_jwtOptions.RememberMeTime),
            claims: claims,
            signingCredentials: SigningCredentials
            );

        return await Task.FromResult(token);
    }

    private async Task<string> GenerateRefreshToken(string key)
    {
        return await Task.FromResult((DateTime.UtcNow.ToString() + key).GetHash());
    }

    private async Task<List<Claim>> GetClaims(User user)
    {

        List<Claim> claims = new()
        {
            new Claim(CustomClaimNames.UserId, user.Id.ToString()),
            new Claim(CustomClaimNames.Role, user.Role?.Id.ToString()),
            new Claim(CustomClaimNames.RoleName, user.Role?.Name),
            new Claim(CustomClaimNames.Permissions,
            string.Join(", ", user.Role?.RolePermissions.Select(p => p.Permission.Code.ToString())))
        };

        return await Task.FromResult(claims);
    }

    public async Task<AccessAndRefreshTokensWithUser> GenerateAccessAndRefreshTokensAsync(User user, bool rememberMe)
    {
        var claims = await GetClaims(user);
        var token = await CreateTokenAsync(claims, rememberMe);

        AccessAndRefreshTokensWithUser tokens = new()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = await GenerateRefreshToken(_jwtOptions.SecretKey),
            ExpiredDate = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenLifeTime),
            RememberMe = rememberMe
        };

        return tokens;
    }

    public Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string expiredToken)
    {
        byte[] key = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);

        var tokenParams = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audiance,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(expiredToken, tokenParams, out SecurityToken securityToken);

        if (securityToken == null)
        {
            throw new SecurityTokenException("Invalid Token");
        }
        return Task.FromResult(claimsPrincipal);
    }
}
