

using Domain.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [CreateService(false)]//added this addtribute because service already created 
    public class News : BaseEntity<int>
    {
        [Column("NewsDate")]
        public DateTime newsDate { get; set; }
        [Column("Title")]
        public string title { get; set; }
        [Column("FileRepoId")]
        public int? fileRepoId { get; set; }
        public virtual FileRepo? fileRepo { get; set; }
        [Column("NewsContent")]
        public string newsContent { get; set; }
        [Column("ValidFrom")]
        public DateTime? validFrom { get; set; }
        [Column("ValidTill")]
        public DateTime? validTill { get; set; }
    }
}


