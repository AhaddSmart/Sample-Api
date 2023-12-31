﻿using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CategoryDtos;

public class CategoryDto : IMapFrom<Category>
{
    public int Id { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public bool isActive { get; set; }
    public int? parentCategoryId { get; set; }
    public CategoryDto? ParentCategory { get; set; }

    //public List<CategoryDto>? ChildCategories { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Category, CategoryDto>();
    }
}
