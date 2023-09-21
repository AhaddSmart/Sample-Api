
using System;
using Application.Common.Mappings;
using Domain.Entities;
using AutoMapper;
using Domain.Enums;
using System.Reflection.Metadata;


namespace Application.DTOs.EnquiryDtos
{
    public class UpdateEnquiryDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public int businessTypeId { get; set; }
        public string loadingPortCountry { get; set; }
        public string loadingPortCity { get; set; }
        public string unloadingPortCountry { get; set; }
        public string unloadingPortCity { get; set; }
        public string detail { get; set; }


    }
}
