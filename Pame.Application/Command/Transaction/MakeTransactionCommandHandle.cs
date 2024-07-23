using MediatR;
using Pame.Domain;

namespace Pame.Application;

public class MakeTransactionCommandHandle : IRequestHandler<MakeTransactionCommand, Response>
{
    private readonly IUnitOfWork _uow;
    private readonly ITransactionRepository _transactionRepository;

    public MakeTransactionCommandHandle(ITransactionRepository transactionRepository, IUnitOfWork uow)
    {
        _transactionRepository = transactionRepository;
        _uow = uow;
    }

    public async Task<Response> Handle(MakeTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = Transaction.Create(request.Holder, request.Description, request.CardNumber, request.CardValidate, request.Cvv, request.Value, request.Method);       
            _transactionRepository.Create(transaction);
            transaction.AddDomainEvent(new TransactionMakeDomainEvent(request.Method, request.Value));
            
            await _uow.Commit();
            return new Response(true, "Success", System.Net.HttpStatusCode.Created);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}
