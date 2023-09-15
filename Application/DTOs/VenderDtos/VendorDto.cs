using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.VendorDtos
{
    public class VendorDto : IMapFrom<Vendor>
    {
        public string name { get; set; }
        public string? lic_no { get; set; }
        public string? taxNo { get; set; }
        public string personName { get; set; }
        public string designation { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string? mobileNo { get; set; }
        public string? website { get; set; }
        public string? mobileNos { get; set; }  //" | | "
        public string? emails { get; set; }  //" | | "
    }
}
