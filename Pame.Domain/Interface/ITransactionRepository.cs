namespace Pame.Domain;

public interface ITransactionRepository
{
    void Create(Transaction transactions);
    Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken);
}
