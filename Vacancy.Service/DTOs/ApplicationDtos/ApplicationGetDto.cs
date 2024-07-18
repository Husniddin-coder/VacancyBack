using Microsoft.AspNetCore.Http;
using Vacancy.Domain.Enums.Applications;

namespace Vacancy.Service.DTOs.ApplicationDtos;

public class ApplicationGetDto
{
    public int Id { get; set; }

    public Status Status { get; set; }

    public string FullName { get; set; }

    public string PassportPath { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string VacancyTitle { get; set; }

    public string VacancyCompany { get; set; }

    public DateTime CreatedDate { get; set; }

    public int VakancyId { get; set; }
}
