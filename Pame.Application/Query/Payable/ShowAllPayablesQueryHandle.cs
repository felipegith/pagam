using MediatR;
using Pame.Domain;

namespace Pame.Application;

public class ShowAllPayablesQueryHandle : IRequestHandler<ShowAllPayablesQuery, Response>
{
    private readonly IPayableRepository _payableRepository;

    public ShowAllPayablesQueryHandle(IPayableRepository payableRepository)
    {
        _payableRepository = payableRepository;
    }

    public async Task<Response> Handle(ShowAllPayablesQuery request, CancellationToken cancellationToken)
    {
        var payables = await _payableRepository.GetAllAsync(cancellationToken);

        if(!payables.Any())
            return new Response(false, "Error", System.Net.HttpStatusCode.NotFound);

        return new Response(true, "Success", System.Net.HttpStatusCode.OK, payables);
    }
}
