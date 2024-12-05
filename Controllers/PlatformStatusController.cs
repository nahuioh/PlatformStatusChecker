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
        
         // Incrementa el contador para este endpoint
        await IncrementCallCounterAsync("GetStatuses");

        var result = await _mediator.Send(new GetPlatformStatusesQuery());
        return Ok(new { statuses = result });
    }
    // Endpoint para obtener el número de llamadas realizadas
    [HttpGet("call-count")]
    public async Task<IActionResult> GetCallCount()
    {
        var counter = await _context.ApiCallCounters
            .FirstOrDefaultAsync(c => c.EndpointName == "GetStatuses");

        return Ok(new
        {
            EndpointName = "GetStatuses",
            CallCount = counter?.CallCount ?? 0
        });
        //return Ok(_platformStatusService.GetCallLogCount());  // Devolvemos la cantidad de llamadas
    }
    private async Task IncrementCallCounterAsync(string endpointName)
    {
        var counter = await _context.ApiCallCounters
            .FirstOrDefaultAsync(c => c.EndpointName == endpointName);

        if (counter == null)
        {
            counter = new ApiCallCounter
            {
                EndpointName = endpointName,
                CallCount = 1
            };
            _context.ApiCallCounters.Add(counter);
        }
        else
        {
            counter.CallCount++;
            _context.ApiCallCounters.Update(counter);
        }

        await _context.SaveChangesAsync();
    }
}
