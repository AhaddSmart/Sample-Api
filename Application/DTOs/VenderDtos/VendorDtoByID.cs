
using System;
using Application.Common.Mappings;
using Domain.Entities;
using AutoMapper;
using Domain.Enums;
using System.Reflection.Metadata;
using Application.DTOs.FileRepoDtos;

namespace Application.DTOs.VenderDtos
{
    public class VendorDtoByID : IMapFrom<Vendor>
    {
        public int Id { get; set; }
        //public int? logoId { get; set; }
        public string? logoPath { get; set; }
        //public FileRepoDto? logoRepo { get; set; }
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
        public string? mobileNos { get; set; }
        public string? emails { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Vendor, VendorDtoByID>().ForMember(x => x.logoPath, y => y.MapFrom(z => z.logoRepo != null ? z.logoRepo.filePath : ""));
        }
    }
}
