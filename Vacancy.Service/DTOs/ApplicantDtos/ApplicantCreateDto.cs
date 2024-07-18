using Microsoft.AspNetCore.Http;

namespace Vacancy.Service.DTOs.ApplicantDtos;

public class ApplicantCreateDto
{
    public string FullName { get; set; }

    public IFormFile PassportFile { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }
}