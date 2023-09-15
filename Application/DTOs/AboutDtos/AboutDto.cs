using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AboutDto : IMapFrom<About>
    {
        public int Id { get; set; }
        public string text { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<About, AboutDto>();
        }
    }
}
