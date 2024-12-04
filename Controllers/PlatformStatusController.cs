using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlatformStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly PlatformStatusService _platformStatusService;
    public PlatformStatusController(IMediator mediator, PlatformStatusService platformStatusService)
    {
        _mediator = mediator;
        _platformStatusService = platformStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlatformStatuses()
    {
        var result = await _mediator.Send(new GetPlatformStatusesQuery());
        return Ok(new { statuses = result });
    }
    // Endpoint para obtener el número de llamadas realizadas
    [HttpGet("call-count")]
    public ActionResult<int> GetCallCount()
    {
        return Ok(_platformStatusService.GetCallLogCount());  // Devolvemos la cantidad de llamadas
    }
}
