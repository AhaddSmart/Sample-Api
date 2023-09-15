
using System;
using Application.Common.Mappings;
using Domain.Entities;
using AutoMapper;
using Domain.Enums;
using System.Reflection.Metadata;

using Application.DTOs.Sample;

namespace Application.DTOs
{
    public class OfferDto : IMapFrom<Offer>
    {
        public int Id { get; set; }
        public string title { get; set; }
        public int? fileRepoId { get; set; }
        public FileRepoDto? fileRepo { get; set; }
    public DateTime? to { get; set; }
    public DateTime? from { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Offer, OfferDto>();
    }
}
 }
            