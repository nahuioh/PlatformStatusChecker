using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlatformStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly PlatformStatusService _platformStatusService;
    private readonly AppDbContext _context;
    public PlatformStatusController(IMediator mediator, PlatformStatusService platformStatusService, AppDbContext context)
    {
        _mediator = mediator;
        _platformStatusService = platformStatusService;
        _context = context;
    }

    [HttpGet("GetStatuses")]
    public async Task<IActionResult> GetPlatformStatuses()
    {
        // Incrementar el contador usando MediatR
        await _mediator.Send(new GetCallCounterQuery { EndpointName = "GetStatuses", Action = "increment" });

        // Obtener los estados de las plataformas
        var result = await _mediator.Send(new GetPlatformStatusesQuery());
        return Ok(new { statuses = result });
    }

    [HttpGet("GetCallCount")]
    public async Task<IActionResult> GetCallCount()
    {
        // Obtener el contador usando MediatR
        var count = await _mediator.Send(new GetCallCounterQuery { EndpointName = "GetStatuses", Action = "get" });

        return Ok(new
        {
            EndpointName = "GetStatuses",
            CallCount = count
        });
    }
}