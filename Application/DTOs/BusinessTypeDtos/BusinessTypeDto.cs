
using Application.Common.Mappings;
using Domain.Entities;
using global::Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BusinessTypeyDtos;

public class BusinessTypeDto : IMapFrom<BusinessType>
{
    public int Id { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public bool isActive { get; set; }
    public int? parentTypeId { get; set; }
    public BusinessTypeDto? ParentType { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<BusinessType, BusinessTypeDto>();
    }
}
