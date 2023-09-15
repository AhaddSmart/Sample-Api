using Application.Common.Interfaces;
using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.NewsDtos;

public class CreateNewsDto : IMapFrom<News>
{
    public DateTime newsDate { get; set; }
    public string title { get; set; }
    public int fileRepoId { get; set; }
    public string newsContent { get; set; }
    public DateTime validFrom { get; set; }
    public DateTime validTill { get; set; }
}