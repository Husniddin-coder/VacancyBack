using Microsoft.AspNetCore.Http;

namespace Vacancy.Service.DTOs.ApplicationDtos;

public class ApplicationUpdateDto
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string OldPassport { get; set; }

    public IFormFile? NewPassportFile { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public int VakancyId { get; set; }
}
