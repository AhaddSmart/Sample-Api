using Domain.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [CreateService(false)]
    public class Offer : BaseEntity<int>
    {
        [Column("Title")]
        public string title { get; set; }
        [Column("VendorId")]
        public int? vendorId { get; set; }
        public virtual Vendor? vendor { get; set; }

        [Column("FileRepoId")]
        public int? fileRepoId { get; set; }
        public virtual FileRepo? fileRepo { get; set; }
        [Column("To")]
        public DateTime? to { get; set; }
        [Column("From")]
        public DateTime? from { get; set; }
    }
}
