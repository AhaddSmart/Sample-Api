using Application.Common.Interfaces;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Entities.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.NewsDtos;

public class CreateNewsDto : IMapFrom<News>
{
    public DateTime NewsDate { get; set; }
    public string Title { get; set; }
    public string FileRepoId { get; set; }
    public string NewsContent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTill { get; set; }
}