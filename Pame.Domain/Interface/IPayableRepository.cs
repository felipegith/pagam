namespace Pame.Domain;

public interface IPayableRepository
{
    void Create(Payable payable);
    Task<Payable> SearchPayableCostumers(Guid id, CancellationToken cancellationToken);
    Task<List<Payable>> GetAllAsync(CancellationToken cancellationToken);

    // Task<List<Payable>> GetAllPaids(string status, CancellationToken cancellationToken);
    // Task<List<Payable>> GetAllWaitingFunds(string status, CancellationToken cancellationToken);
    Task<List<Payable>> GetPayablesForStatus(string status, CancellationToken cancellationToken);
}
