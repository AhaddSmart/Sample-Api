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
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

public class BannerController : ApiControllerBase
{
    private readonly ILogger<BannerController> _logger;
    public BannerController(ILogger<BannerController> logger)
    {
        _logger = logger;
    }
    //[Authorize()]
    [HttpGet]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Get()
    {
        _logger.LogInformation("GetBanner method started");
        
            //return await Mediator.Send(new GetBannersQuery());
            var result = await Mediator.Send(new GetBannersQuery());
        if(result.status == 0)
            _logger.LogError("GetBanner : "+ result.error);

        return result;
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

