using MediatR;
using Pame.Domain;

namespace Pame.Application;

public class ShowAllPayablesForStatusHandle : IRequestHandler<ShowAllPayablesForStatus, Response>
{
    private readonly IPayableRepository _payableRepository;

    public ShowAllPayablesForStatusHandle(IPayableRepository payableRepository)
    {
        _payableRepository = payableRepository;
    }

    public async Task<Response> Handle(ShowAllPayablesForStatus request, CancellationToken cancellationToken)
    {
        var payable = new Payable();

        var status = payable.GetStatusPayable(request.Status);
        var payablesPaid = await _payableRepository.GetPayablesForStatus(status, cancellationToken);

        if(!payablesPaid.Any())
            return new Response(false, "Error", System.Net.HttpStatusCode.NotFound);

        return new Response(true, "Success", System.Net.HttpStatusCode.OK, payablesPaid);
    }
}
