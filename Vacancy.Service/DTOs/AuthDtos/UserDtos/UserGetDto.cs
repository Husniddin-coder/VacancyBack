using System.ComponentModel.DataAnnotations.Schema;

namespace Vacancy.Service.DTOs.AuthDtos.UserDtos;

public class UserGetDto
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string? Image { get; set; }

    public string? Company { get; set; } // company should be another table actually

    public string? Region { get; set; }

    public bool? IsApproved { get; set; }
}
