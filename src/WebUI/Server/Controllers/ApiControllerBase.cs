using RapidBlazor.WebUI.Server.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RapidBlazor.WebUI.Server.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
