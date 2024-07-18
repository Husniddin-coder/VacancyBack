namespace Vacancy.Service.DTOs.VacancyDtos;

public class VakancyUpdateDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Salary { get; set; }

    public string Region { get; set; } // added by me

    public string Company { get; set; } // added by me

    public string WorkingDays { get; set; }

    public string QualificationRequirements { get; set; }

    public DateTime JobStartDate { get; set; }
}
