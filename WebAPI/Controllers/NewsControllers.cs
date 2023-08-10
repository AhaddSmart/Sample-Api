using Application.Common.Exceptions;
using Application.Common.Models;
using Application.DTOs.NewsDtos;
using Application.Services.News.Commands;
using Application.Services.News.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;

namespace WebAPI.Controllers
{
    public class NewsController : ApiControllerBase
    {
        [HttpGet]
        [Route("v1/[controller]")]
        public async Task<ResponseHelper> Get()
        {
            return await Mediator.Send(new GetNewsQuery());
        }
        [HttpGet]
        [Route("v1/[controller]/{Id}")]
        public async Task<ResponseHelper> GetNewsById(int Id)
        {
            return await Mediator.Send(new GetNewsByIdQuery { Id = Id });
        }
        [HttpPost]
        [Route("v1/[controller]")]
        public async Task<ResponseHelper> Create()
        {
            CreateNewsCommand command = new ();
            command.formRequest = Request;
            return await Mediator.Send(command);
        }
        [HttpPut("v1/[controller]")]
        public async Task<ResponseHelper> Update()
        {
            UpdateNewsCommand command = new();
            command.formRequest = Request;
            return await Mediator.Send(command);
        }

        [HttpDelete("v1/[controller]/{id}")]
        public async Task<ResponseHelper> Delete(int id, DeleteNewsCommand command)
        {
            if (id != command.Id)
            {
                throw new NotFoundException("News entity not found.");
            }
            return await Mediator.Send(command);

        }
    }
}
