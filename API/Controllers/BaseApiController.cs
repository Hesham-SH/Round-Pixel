using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers;

[EnableRateLimiting("FixedWindowPolicy")]
[ApiController]
[Route("[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??=
    HttpContext.RequestServices.GetService<IMediator>();
}
