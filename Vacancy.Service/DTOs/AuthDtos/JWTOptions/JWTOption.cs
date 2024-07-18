namespace Vacancy.Service.DTOs.AuthDtos.JWTOptions;

public class JWTOption
{
    public string Issuer { get; set; }

    public string Audiance { get; set; }

    public string SecretKey { get; set; }

    public double AccessTokenLifeTime { get; set; }

    public double RefreshTokenLifeTime { get; set; }

    public double RememberMeTime { get; set; }
}
