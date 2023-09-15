using Application.Common.Mappings;
using Application.DTOs.OfferDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.VenderDtos
{
    public class CreateVendorDto : IMapFrom<Vendor>
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
        public string logoPath{ get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Vendor, CreateVenderDto>().ForMember(x => x.path, y => y.MapFrom(z => z.logoRepo != null ? z.logoRepo.filePath : ""));
        }
    }
}
