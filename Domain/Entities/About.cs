using Domain.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [CreateService(false)]
    public class About : BaseEntity<int>
    {
        [Column("Text")]
        public string? text { get; set; }
    }
}


