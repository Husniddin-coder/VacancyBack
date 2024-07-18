using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;
using Vacancy.Domain.Entities.Applicants;
using Vacancy.Domain.Entities.Vacancies;
using Vacancy.Domain.Enums.Applications;

namespace Vacancy.Domain.Entities.Applications;

[Table("applications")]
public class Application : Auditable
{
    [Column("status")]
    public Status Status { get; set; } = Status.Submitted;

    [Column("applicant_id")]
    [ForeignKey(nameof(Applicant))]
    public int ApplicantId { get; set; }
    public virtual Applicant Applicant { get; set; }

    [Column("vakancy_id")]
    [ForeignKey(nameof(Vakancy))]
    public int VakancyId { get; set; }
    public virtual Vakancy Vakancy { get; set; }
}

public class AppStatus
{
    public string Name { get; set; }

    public string Code { get; set; }
}
