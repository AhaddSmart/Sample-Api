﻿using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BannerDto;

public class BannerDto : IMapFrom<Banner>
{
    public int Id { get; set; }
    public string title { get; set; }
    //public string fileRepoId { get; set; }
    public string? path { get; set;}

    //public DateTime? from { get; set; }
    //public DateTime? to { get; set; }
    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Banner, BannerDto>().ForMember(x => x.path, y => y.MapFrom(z => z.fileRepo != null ? z.fileRepo.filePath: ""));
    }
}

//public class BannerDataDto : BannerDto
//{
//    public DateTime? from { get; set; }
//    public DateTime? to { get; set; }
//    public void Mapping(MappingProfile profile)
//    {
//        profile.CreateMap<Banner, BannerDto>();
//    }
//}
