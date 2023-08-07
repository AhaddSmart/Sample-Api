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
    public DateTime NewsDate { get; set; }
    public string Title { get; set; }
    public string FileRepoId { get; set; }
    public string NewsContent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTill { get; set; }
    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<News, NewsDto>();
    }


}