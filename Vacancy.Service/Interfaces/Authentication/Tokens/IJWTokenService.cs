using System.Security.Claims;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Service.Interfaces.Authentication.Tokens;

public interface IJWTokenService
{
    public Task<AccessAndRefreshTokensWithUser> GenerateAccessAndRefreshTokensAsync(User user, bool rememberMe);

    public Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string expiredToken);
}
