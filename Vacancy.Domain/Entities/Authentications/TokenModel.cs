using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;

namespace Vacancy.Domain.Entities.Authentications;

[Table("token_model", Schema = "auth")]
public class TokenModel : Auditable
{
    [Column("user_id")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    [NotMapped]
    public virtual User User { get; set; }

    [Column("access_token")]
    public string AccessToken { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("remember_me")]
    public bool RememberMe { get; set; } = false;

    [Column("expired_date_of_refresh_token")]
    public DateTime ExpiredDateOfRefreshToken { get; set; }
}
