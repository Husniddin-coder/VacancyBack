using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Vacancy.Domain.Commons;

namespace Vacancy.Domain.Entities.Authentications;

[Table("users", Schema = "auth")]
public class User : Auditable
{
    [Column("username")]
    public string UserName { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("avatar_image")]
    public string? Image { get; set; }

    [Column("company_name")]
    public string? Company { get; set; } // company should be another table actually

    [Column("region")]
    public string? Region { get; set; }

    [Column("is_approved")]
    public bool? IsApproved { get; set; }

    [Column("role_id")]
    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }

    [JsonIgnore]
    public virtual Role Role { get; set; }
}
