using Application.Common.Models;
using Application.DTOs;
using Application.Services.Enquiries.Queries;
using Application.Services.Enquiries.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

//[Authorize]
public class EnquiryController : ApiControllerBase
{

    [HttpGet]
    [Route("v1/[controller]")]
    public async Task<ResponseHelper> GetEnquiry()
    {
        return await Mediator.Send(new GetEnquiriesQuery());
    }

    [HttpGet]
    [Route("v1/[controller]/{Id}")]
    public async Task<ResponseHelper> GetEnquiryByID(int Id)
    {
        return await Mediator.Send(new GetEnquiryQueryByID { Id = Id });
    }

    [HttpPost]
    [Route("v1/[controller]/")]
    public async Task<ResponseHelper> CreateEnquiry(CreateEnquiryCommand command)
    {
        return await Mediator.Send(command);
    }


    //[HttpDelete(@"Enquiries/v1/DeleteEnquiry")]
    //public async Task<ResponseHelper> DeleteEnquiry(int Id)
    //{
    //    return await Mediator.Send(new DeleteEnquiryCommand { id = Id });
    //}

    //[HttpPut(@"Enquiries/v1/UpdateEnquiry")]
    //public async Task<ResponseHelper> UpdateEnquiry(UpdateEnquiryCommand command)
    //{
    //     return await Mediator.Send(command);
    //}

}
