using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;

namespace Vacancy.Domain.Entities.Authentications;

[Table("roles", Schema = "auth")]
public class Role : Auditable
{
    [Column("role_name")]
    public string Name { get; set; }

    [Column("is_default")]
    public bool isDefault { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
