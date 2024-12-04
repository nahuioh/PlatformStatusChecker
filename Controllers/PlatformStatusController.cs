using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlatformStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlatformStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlatformStatuses()
    {
        var result = await _mediator.Send(new GetPlatformStatusesQuery());
        return Ok(new { statuses = result });
    }
}
