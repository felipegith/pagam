using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pame.Application;

namespace Pame.Api;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapster;

    public TransactionController(IMediator mediator, IMapper mapster)
    {
        _mediator = mediator;
        _mapster = mapster;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TransactionInput model, CancellationToken cancellationToken)
    {
        var command = _mapster.Map<MakeTransactionCommand>(model);
        var result = await _mediator.Send(command, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new ShowAllTransactionsQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
