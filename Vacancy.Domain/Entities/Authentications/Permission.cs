using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;

namespace Vacancy.Domain.Entities.Authentications;

[Table("permissions", Schema = "auth")]
public class Permission : Auditable
{
    [Column("permission_name")]
    public string Name { get; set; }

    [Column("code")]
    public int Code { get; set; }
}
