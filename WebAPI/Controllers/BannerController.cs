using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Services.News.Commands;
using Application.Services.News.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;
using Application.Services.Banner.Queries;
using Application.Services.Banner.Commands;
using Application.Services.Banners.Commands;

namespace WebAPI.Controllers;

public class BannerController : ApiControllerBase
{
    //[Authorize()]
    [HttpGet]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Get()
    {
        return await Mediator.Send(new GetBannersQuery());
    }
    [HttpGet]
    [Route("v1/[controller]/{Id}")]
    public async Task<ResponseHelper> GetBannerById(int Id)
    {
        return await Mediator.Send(new GetBannerByIdQuery { Id = Id });
    }
    [HttpPost]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Create()
    {
        CreateBannerCommand command = new();
        command.formRequest = Request;
        return await Mediator.Send(command);
    }
    //[HttpPut("v1/[controller]")]
    //public async Task<ResponseHelper> Update()
    //{
    //    UpdateOfferCommand command = new();
    //    command.formRequest = Request;
    //    return await Mediator.Send(command);
    //}

    [HttpDelete("v1/[controller]/{id}")]
    public async Task<ResponseHelper> Delete(int id, DeleteBannerCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("Banner entity not found.");
        }
        return await Mediator.Send(command);

    }
}

