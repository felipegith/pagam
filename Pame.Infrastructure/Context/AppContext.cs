using Microsoft.EntityFrameworkCore;
using Pame.Domain;

namespace Pame.Infrastructure;

public class AppContext : DbContext
{
    private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;
    public AppContext(DbContextOptions<AppContext> options, PublishDomainEventInterceptor publishDomainEventInterceptor) : base(options)
    {
        _publishDomainEventInterceptor = publishDomainEventInterceptor;
    }

    public DbSet<Payable> Payables {get; protected set;}
    public DbSet<Transaction> Transactions {get; protected set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Ignore<List<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
       
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
