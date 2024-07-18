using System.ComponentModel.DataAnnotations.Schema;
using Vacancy.Domain.Commons;

namespace Vacancy.Domain.Entities.Assetss;

[Table("assets", Schema = "assets")]
public class Assets : Auditable
{
    [Column("name")]
    public string Name { get; set; }

    [Column("path")]
    public string Path { get; set; }

    [Column("extention")]
    public string Extention { get; set; }

    [Column("size")]
    public long Size { get; set; }

    [Column("type")]
    public string Type { get; set; }
}