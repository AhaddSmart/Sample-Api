using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Services.Vendors.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;

namespace WebAPI.Controllers;

public class VenderController : ApiControllerBase
{
    //[Authorize()]
    //[HttpGet]
    //[Route("v1/[controller]")]
    //public async Task<ResponseHelper> Get()
    //{
    //    return await Mediator.Send(new GetOffersQuery());
    //}
    //[HttpGet]
    //[Route("v1/[controller]/{Id}")]
    //public async Task<ResponseHelper> GetOfferById(int Id)
    //{
    //    return await Mediator.Send(new GetOfferByIdQuery { Id = Id });
    //}
    [HttpPost]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> Create()
    {
        CreateVendorCommand command = new();
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

    //[HttpDelete("v1/[controller]/{id}")]
    //public async Task<ResponseHelper> Delete(int id, DeleteOfferCommand command)
    //{
    //    if (id != command.Id)
    //    {
    //        throw new NotFoundException("Offer entity not found.");
    //    }
    //    return await Mediator.Send(command);

    //}
}

