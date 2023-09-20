using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OfferDtos;
public class OfferDto : IMapFrom<Offer>
{
    public int Id { get; set; }
    public string title { get; set; }
    //public string fileRepoId { get; set; }
    //public DateTime? from { get; set; }
    //public DateTime? to { get; set; }
    //public int? vendorId { get; set; }
    public string? vendorName { get; set; }
    public string? path { get; set; }
    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Offer, OfferDto>()
            .ForMember(x => x.path, y => y.MapFrom(z => z.fileRepo != null ? z.fileRepo.filePath : ""))
            .ForMember(x => x.vendorName, y => y.MapFrom(z => z.vendor != null ? z.vendor.name: ""));
    }
}

