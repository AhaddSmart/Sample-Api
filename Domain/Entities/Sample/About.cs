using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Sample;
public class About : BaseEntity<int>
{
    [Column("Text")]
    public string? text { get; set; }
}

