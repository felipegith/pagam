using Microsoft.EntityFrameworkCore;
using Pame.Domain;

namespace Pame.Infrastructure;

public class PayableRepository : IPayableRepository
{
    private readonly AppContext _context;

    public PayableRepository(AppContext context)
    {
        _context = context;
    }
    public void Create(Payable payable)
     => _context.Add(payable);

    public async Task<Payable> SearchPayableCostumers(Guid id, CancellationToken cancellationToken)
        => await _context.Payables.FindAsync(id) ?? new Payable();

    public async Task<List<Payable>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Payables.AsNoTracking().ToListAsync();

        public async Task<List<Payable>> GetPayablesForStatus(string status, CancellationToken cancellationToken)
        => await _context.Payables.AsNoTracking().Where(x=>x.PayableStatus == status).ToListAsync();
}
