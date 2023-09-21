using Domain.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [CreateService(false)]
    public class Vendor : BaseEntity<int>
    {
        [Column("LogoId")]
        [ForeignKey("logoRepo")]
        public int? logoId { get; set; }
        public virtual FileRepo? logoRepo { get; set; }
        [Column("Name")]
        public string name { get; set; }
        [Column("LIC_No")]
        public string? lic_no { get; set; }
        [Column("TaxNo")]
        public string? taxNo { get; set; }
        [Column("PersonName")]
        public string personName { get; set; }
        [Column("Designation")]
        public string designation { get; set; }
        [Column("Country")]
        public string country { get; set; }
        [Column("City")]
        public string city { get; set; }
        [Column("Address")]
        public string address { get; set; }
        [Column("Email")]
        public string email { get; set; }
        [Column("MobileNo")]
        public string? mobileNo { get; set; }
        [Column("Website")]
        public string? website { get; set; }
        [Column("MobileNos")]
        public string? mobileNos { get; set; }  //" | | "
        [Column("Emails")]
        public string? emails { get; set; }  //" | | "
    }
}