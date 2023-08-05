using Application.Common.Exceptions;
using Application.Common.Models;
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
    public async Task<ResponseHelper> Get()
    {
        return await Mediator.Send(new GetCategoryQuery());
    }

    [HttpGet]
    [Route("v1/[controller]/{Id}")]
    public async Task<ResponseHelper> GetCategoryById(int Id)
    {
        return await Mediator.Send(new GetCategoryByIdQuery { Id = Id });
    }
    [HttpGet]
    [Route("v1/[controller]/parent/{Id}")]
    public async Task<ResponseHelper> GetCategoryByParentId(int Id)
    {
        return await Mediator.Send(new GetCategoryByParentIdQuery { ParentCategoryId = Id });
    }

    [HttpPost]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Create(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("v1/[controller]/{id}")]
    public async Task<ResponseHelper> Update(int id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("Category entity not found."); ;
        }
        return await Mediator.Send(command);
    }

    [HttpDelete("v1/[controller]/{id}")]
    public async Task<ResponseHelper> Delete(int id, DeleteCategoryCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("category entity not found.");
        }
        return await Mediator.Send(command);
    }
}
