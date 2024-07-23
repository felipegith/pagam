using MediatR;

namespace Pame.Application;

public record ShowAllTransactionsQuery : IRequest<Response>;

