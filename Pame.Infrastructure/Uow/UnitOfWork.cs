using Pame.Domain;

namespace Pame.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
     private readonly AppContext _context;

    public UnitOfWork(AppContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        var success = (await _context.SaveChangesAsync()) > 0;

        return success;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
