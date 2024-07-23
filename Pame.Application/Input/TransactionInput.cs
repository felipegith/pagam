using Pame.Domain;

namespace Pame.Application;

public record TransactionInput(string Holder, decimal Value, string Description, Method Method, long CardNumber, DateTime CardValidate, int Cvv);

