using Application.Common.Exceptions;
using Application.DTOs;
using Application.DTOs.CategoryDtos;
using Application.DTOs.NewsDtos;
using Application.Services.Categories.Commands;
using Application.Services.Categories.Queries;
using Application.Services.News.Commands;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;

namespace WebAPI.Controllers;

public class CategoryController : ApiControllerBase
{
    [HttpGet]
    [Route("v1/[controller]")]
    public async Task<IEnumerable<CategoryDto>> Get()
    {
        return await Mediator.Send(new GetCategoryQuery());
    }

    [HttpGet]
    [Route("v1/[controller]/{Id}")]
    public async Task<CategoryDto> GetCategoryById(int Id)
    {
        return await Mediator.Send(new GetCategoryByIdQuery { Id = Id });
    }
    [HttpGet]
    [Route("v1/[controller]/child/{ParentCategoryId}")]
    public async Task<List<CategoryDto>> GetCategoryByParentId(int ParentCategoryId)
    {
        return await Mediator.Send(new GetCategoryByParentIdQuery { ParentCategoryId = ParentCategoryId });
    }

    [HttpPost]
    [Route("v1/[controller]")]
    public async Task<CategoryDto> Create(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("v1/[controller]/{id}")]
    public async Task<CategoryDto> Update(int id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("Category entity not found."); ;
        }
        return await Mediator.Send(command);
    }

    [HttpDelete("v1/[controller]/{id}")]
    public async Task<bool> Delete(int id, DeleteCategoryCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("category entity not found.");
        }
        return await Mediator.Send(command);
    }
}
