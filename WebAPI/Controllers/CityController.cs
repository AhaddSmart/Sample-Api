using Application.DTOs;
using Application.Services.Cities.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

//[Authorize]
public class CityController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<CityDto>> Get()
    {
        return await Mediator.Send(new GetCitiesQuery());
    }


    //[HttpPost]
    //public async Task<ActionResult<int>> Create(CreateProjectCommand command)
    //{
    //    return await Mediator.Send(command);
    //}

    //[HttpPut("{id}")]
    //public async Task<ActionResult> Update(int id, UpdateProjectCommand command)
    //{
    //    if (id != command.Id)
    //    {
    //        return BadRequest();
    //    }

    //    await Mediator.Send(command);

    //    return NoContent();
    //}
    
    //[HttpDelete("{id}")]
    //public async Task<ActionResult> Delete(int id)
    //{
    //    await Mediator.Send(new DeleteProjectCommand { Id = id });

    //    return NoContent();
    //}
}
