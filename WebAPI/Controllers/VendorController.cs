using Application.Common.Models;
using Application.DTOs;
using Application.Services.Vendors.Queries;
using Application.Services.Vendors.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

//[Authorize]
public class VendorController : ApiControllerBase
{

    [HttpGet("v1/[controller]")]
    public async Task<ResponseHelper> GetVendor()
    {
        return await Mediator.Send(new GetVendorsQuery());
    }

    [HttpGet("v1/[controller]/{id}")]
    public async Task<ResponseHelper> GetVendorByID(int Id)
    {
        return await Mediator.Send(new GetVendorQueryByID { Id = Id });
    }
    
    [HttpPost("v1/[controller]")]
    public async Task<ResponseHelper> CreateVendor()
    {
        CreateVendorCommand command = new();
        command.formRequest = Request;
        return await Mediator.Send(command);
    }
    
    
    [HttpDelete("v1/[controller]")]
    public async Task<ResponseHelper> DeleteVendor(int Id)
    {
        return await Mediator.Send(new DeleteVendorCommand { id = Id });
    }

    [HttpPut("v1/[controller]")]
    public async Task<ResponseHelper> UpdateVendor(UpdateVendorCommand command)
    {
         return await Mediator.Send(command);
    }

}
