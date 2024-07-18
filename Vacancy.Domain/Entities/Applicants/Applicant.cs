using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;
using Vacancy.Domain.Entities.Applications;
using Vacancy.Domain.Entities.Authentications;

namespace Vacancy.Domain.Entities.Applicants;

[Table("applicants")]
public class Applicant : Auditable
{
    [Column("full_name")]
    public string FullName { get; set; }

    [Column("passport_path")]
    public string PassportPath { get; set; }

    [Column("phone_number")]
    public string PhoneNumber { get; set; }

    [Column("address")]
    public string Address { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("user_id")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Application> Applications { get; set; }
}
