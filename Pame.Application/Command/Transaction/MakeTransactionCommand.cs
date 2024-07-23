using MediatR;
using Pame.Domain;

namespace Pame.Application;

public record MakeTransactionCommand(string Holder, decimal Value, string Description, Method Method, long CardNumber, DateTime CardValidate, int Cvv ) : IRequest<Response>;
