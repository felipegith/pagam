using MediatR;
using Pame.Domain;

namespace Pame.Application;

public class ShowAllTransactionsQueryHandle : IRequestHandler<ShowAllTransactionsQuery, Response>
{
    private readonly ITransactionRepository _transactionRepository;

    public ShowAllTransactionsQueryHandle(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Response> Handle(ShowAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);

        if(!transactions.Any())
          return new Response(false, "Error", System.Net.HttpStatusCode.NotFound);

        return new Response(true, "Success", System.Net.HttpStatusCode.OK, transactions);
    }
}
