using Microsoft.EntityFrameworkCore;
using Pame.Domain;

namespace Pame.Infrastructure;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppContext _context;

    public TransactionRepository(AppContext context)
    {
        _context = context;
    }

    public void Create(Transaction transactions)
     => _context.Add(transactions);

    public async Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken)
    => await _context.Transactions.AsNoTracking().ToListAsync();
}
