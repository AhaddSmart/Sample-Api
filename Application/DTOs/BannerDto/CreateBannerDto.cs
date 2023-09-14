using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BannerDto;
public class CreateBannerDto : IMapFrom<Banner>
{
    public string title { get; set; }
    //public int fileRepoId { get; set; }
    public DateTime from { get; set; }
    public DateTime to { get; set; }
    public string? path { get; set; }

    //public DateTime? from { get; set; }
    //public DateTime? to { get; set; }
    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Banner, CreateBannerDto>().ForMember(x => x.path, y => y.MapFrom(z => z.fileRepo != null ? z.fileRepo.filePath : ""));
    }
}
