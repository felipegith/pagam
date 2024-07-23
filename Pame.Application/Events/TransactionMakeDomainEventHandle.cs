using MediatR;
using Pame.Domain;

namespace Pame.Application;

public class TransactionMakeDomainEventHandle : INotificationHandler<TransactionMakeDomainEvent>
{
    private readonly IPayableRepository _payableRepository;
    private readonly IUnitOfWork _uow;
    public TransactionMakeDomainEventHandle(IPayableRepository payableRepository, IUnitOfWork uow)
    {
        _payableRepository = payableRepository;
        _uow = uow;
    }

    public async Task Handle(TransactionMakeDomainEvent notification, CancellationToken cancellationToken)
    {
        var payable = new Payable();
        var status = payable.SetPayableStatus(notification.Method);
        var paymentDate = payable.SetPaymentDate(notification.Method);
        var value = payable.CalculateRate(notification.Value, notification.Method);

        var create = Payable.Create(status, paymentDate, value, notification.Method);

        _payableRepository.Create(create);
        await _uow.Commit();
    }
}
