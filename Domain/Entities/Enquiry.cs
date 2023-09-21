using Domain.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //[CreateService(false)]
    public class Enquiry : BaseEntity<int>
    {
        [Column("Title")]
        public string title { get; set; }

        [Column("BusinessTypeId")]
        [ForeignKey("businessType")]
        public int businessTypeId { get; set; }
        public virtual BusinessType? businessType { get; set; }

        [Column("LoadingPortCountry")]
        public string loadingPortCountry { get; set; }

        [Column("LoadingPortCity")]
        public string loadingPortCity { get; set; }

        [Column("UnloadingPortCountry")]
        public string unloadingPortCountry { get; set; }

        [Column("UnloadingPortCity")]
        public string unloadingPortCity { get; set; }

        [Column("Detail")]
        public string detail { get; set; }


        //[Column("CompanyLogoId")]
        //[ForeignKey("companyLogoRepo")]
        //public int? companyLogoId { get; set; }
        //public virtual FileRepo? companyLogoRepo { get; set; }

        //[Column("PersonProfileImageId")]
        //[ForeignKey("personProfileImageRepo")]
        //public int? personProfileImageId { get; set; }
        //public virtual FileRepo? personProfileImageRepo { get; set; }
        //[Column("firstName")]
        //public string firstName { get; set; }
        //[Column("LastName")]
        //public string LastName { get; set; }
        //[Column("designation")]
        //public string designation { get; set; }
        //[Column("telephone")]
        //public string telephone { get; set; }
        //[Column("mobile")]
        //public string mobile { get; set; }
        //[Column("email")]
        //public string email { get; set; }
        //[Column("country")]
        //public string countr { get; set; }
        //[Column("city")]
        //public string cit { get; set; }
        //[Column("companyAddress")]
        //public string companyAddress { get; set; }
        //[Column("userName")]
        //public string userName { get; set; }
        //[Column("password")]
        //public string password { get; set; }
        //[Column("companyName")]
        //public string companyName { get; set; }
        //[Column("CompanyRegistrationNo")]
        //public string CompanyRegistrationNo { get; set; }
        //[Column("website")]
        //public string website { get; set; }
        //[Column("businessType")]
        //public string businessTyp { get; set; }
    }
}
