using System.ComponentModel.DataAnnotations.Schema;

namespace Vacancy.Domain.Commons;

public abstract class Auditable
{
    [Column("id")]
    public int Id { get; set; }

    [Column("is_archived")]
    public bool IsArchived { get; set; } = false;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}