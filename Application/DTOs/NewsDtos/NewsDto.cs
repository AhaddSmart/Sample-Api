using Application.Common.Mappings;
using Domain.Entities;
using Domain.Entities.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.NewsDtos;

public class NewsDto : IMapFrom<News>
{
    public int Id { get; set; }
    public DateTime newsDate { get; set; }
    public string title { get; set; }
    public string fileRepoId { get; set; }
    public string newsContent { get; set; }
    public DateTime validFrom { get; set; }
    public DateTime validTill { get; set; }
    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<News, NewsDto>();
    }


}