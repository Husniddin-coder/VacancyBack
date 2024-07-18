namespace Vacancy.Service.DTOs.AuthDtos.TokenDtos;

public class TokenGetDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string AccessToken { get; set; }

    public string RefrshToken { get; set; }

    public DateTime ExpiredDateOfRefreshToken { get; set; }
}
