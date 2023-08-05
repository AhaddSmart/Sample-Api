﻿using Application.Common.Exceptions;
using Application.Common.Models;
using Application.DTOs;
using Application.Services.About.Commands;
using Application.Services.About.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers;

//[Authorize]
public class AboutController : ApiControllerBase
{
    [HttpGet]
    [Route("v1/[controller]")]
    public async Task<IEnumerable<AboutDto>> Get()
    {
        return await Mediator.Send(new GetAboutQuery());
    }
    [HttpPost]
    [Route("v1/[controller]")]
    public async Task<AboutDto> Create(CreateAboutCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpPut("v1/[controller]/{id}")]
    public async Task<AboutDto> Update(int id, UpdateAboutCommand command)
    {
        if (id != command.Id)
        {
             throw new NotFoundException("About entity not found."); ;
        }

        return await Mediator.Send(command);
        
    }

    [HttpDelete("v1/[controller]/{id}")]
    public async Task<bool> Delete(int id, DeleteAboutCommand command)
    {
        if (id != command.Id)
        {
            throw new NotFoundException("About entity not found."); ;
        }
        return await Mediator.Send(command);

    }
}