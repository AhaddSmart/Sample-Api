using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OfferDtos;
public class CreateOfferDto : IMapFrom<Offer>
{
    public string title { get; set; }
    public int fileRepoId { get; set; }
    public DateTime from { get; set; }
    public DateTime to { get; set; }
}


