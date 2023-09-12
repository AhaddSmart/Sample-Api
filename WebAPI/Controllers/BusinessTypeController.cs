using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Services.BusinessCategories.Commands;
using Application.Services.BusinessCategories.Queries;
using Application.Services.BusinesTypes.Commands;
using Application.Services.Categories.Commands;
using Application.Services.Categories.Queries;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;

namespace WebAPI.Controllers;

public class BusinessTypeController : ApiControllerBase
{
    [HttpGet]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Get()
    {
        return await Mediator.Send(new GetBusinessTypeQuery());
    }

    [HttpGet]
    [Route("v1/[controller]/{Id}")]
    public async Task<ResponseHelper> GetBusinessTypeById(int Id)
    {
        return await Mediator.Send(new GetBusinessTypeByIdQuery { Id = Id });
    }
    //[HttpGet]
    //[Route("v1/[controller]/parent/{Id}")]
    //public async Task<ResponseHelper> GetCategoryByParentId(int Id)
    //{
    //    return await Mediator.Send(new GetCategoryByParentIdQuery { parentCategoryId = Id });
    //}

    [HttpPost]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Create(CreateBusinessTypeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("v1/[controller]/{id}")]
    public async Task<ResponseHelper> Update(int id, UpdateBusinessTypeCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("Business Type entity not found."); ;
        }
        return await Mediator.Send(command);
    }

    [HttpDelete("v1/[controller]/{id}")]
    public async Task<ResponseHelper> Delete(int id, DeleteBusinessTypeCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("Business Type entity not found.");
        }
        return await Mediator.Send(command);
    }
}
