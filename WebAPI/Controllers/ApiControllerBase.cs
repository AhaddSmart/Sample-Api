using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
//[Route("api/[controller]")]
[Route("api/")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
