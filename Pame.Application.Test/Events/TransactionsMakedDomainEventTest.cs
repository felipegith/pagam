using NSubstitute;
using Pame.Domain;

namespace Pame.Application.Test;

public class TransactionsMakedDomainEventTest
{
    private static readonly TransactionMakeDomainEvent transactionMakeDomainEventMoq = new (Method.Debit, 1000 );
    private readonly TransactionMakeDomainEventHandle _handle;
    private readonly IPayableRepository _payableRepositoryMoq;
    private readonly IUnitOfWork _uowMoq;
    public TransactionsMakedDomainEventTest()
    {
        _payableRepositoryMoq = Substitute.For<IPayableRepository>();
        _uowMoq = Substitute.For<IUnitOfWork>();
        _handle = new TransactionMakeDomainEventHandle(_payableRepositoryMoq, _uowMoq);
    }

    [Fact]
    public void Event_Domain_Should_Be_Add_Payable_In_Database_And_Return_Success()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;

        var payable = Payable.Create(status, paymentDate, value, method);

        _payableRepositoryMoq.Create(payable);
        var result = _handle.Handle(transactionMakeDomainEventMoq, default);

        _payableRepositoryMoq.Received(1).Create(payable);
        
    }
}
