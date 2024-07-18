namespace Vacancy.Service.DTOs.AuthDtos.AccountDtos;

public class RegisterDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsEmployer { get; set; }

    public string? Company { get; set; }

    public string? Region { get; set; }
}
