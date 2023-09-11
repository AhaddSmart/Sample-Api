
using Application.Common.Mappings;
using Domain.Entities;
using global::Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BusinessCategoryDtos;

public class BusinessCategoryDto : IMapFrom<BusinessCategory>
{
    public int Id { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public bool isActive { get; set; }
    public int? parentCategoryId { get; set; }
    public BusinessCategoryDto? ParentCategory { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<BusinessCategory, BusinessCategoryDto>();
    }
}
