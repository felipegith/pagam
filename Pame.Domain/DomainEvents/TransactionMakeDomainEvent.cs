namespace Pame.Domain;

public record TransactionMakeDomainEvent(Method Method, decimal Value) : IDomainEvent;

