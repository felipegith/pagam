using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pame.Application;
using Pame.Domain;

namespace Pame.Api;

[ApiController]
[Route("[controller]")]
public class PayableController : ControllerBase
{
    private readonly IMediator _mediator;
    

    public PayableController(IMediator mediator)
    {
        _mediator = mediator;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new ShowAllPayablesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("{status}")]
    public async Task<IActionResult> GetAllPaids(Status status,CancellationToken cancellationToken)
    {
        var query = new ShowAllPayablesForStatus(status);
        var result = await _mediator.Send(query, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
