using Microsoft.AspNetCore.Http;

namespace Vacancy.Service.DTOs.ApplicantDtos;

public class ApplicantUpdateDto
{
    public string FullName { get; set; }

    public string OldPassport { get; set; }

    public IFormFile? NewPassportFile { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }
}
