using System.ComponentModel.DataAnnotations.Schema;

namespace Vacancy.Service.Interfaces.Authentication.Tokens;

public class AccessAndRefreshTokensWithUser
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpiredDate { get; set; }

    public bool RememberMe { get; set; } = false;

    public UserResponse User { get; set; }
}

public class AccessAndRefreshTokens 
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpiredDate { get; set; }
}


public class UserResponse
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string? Image { get; set; }

    public string? Company { get; set; } // company should be another table actually

    public string? Region { get; set; }

    public bool? IsApproved { get; set; }
}
