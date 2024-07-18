using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;
using Vacancy.Domain.Entities.Applications;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Domain.Entities.Vacancies;

[Table("vacancy")]
public class Vakancy : Auditable
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("salary")]
    public decimal Salary { get; set; }

    [Column("region")]
    public string Region { get; set; } // added by me

    [Column("company")]
    public string Company { get; set; } // added by me

    [Column("working_days")]
    public string WorkingDays { get; set; }

    [Column("qualification_requirements")]
    public string QualificationRequirements { get; set; }

    [Column("job_start_date")]
    public DateTime JobStartDate { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Application> Applications { get; set; }
}