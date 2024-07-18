using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;

namespace Vacancy.Domain.Entities.Authentications;

[Table("role_permissions", Schema = "auth")]
public class RolePermission : Auditable
{
    [Column("role_id")]
    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }

    public virtual Role Role { get; set; }

    [Column("permission_id")]
    [ForeignKey(nameof(Permission))]
    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; }

    [Column("givenby_id")]
    [ForeignKey(nameof(GivenBy))]
    public int? GivenById { get; set; }

    public virtual User GivenBy { get; set; }
}
