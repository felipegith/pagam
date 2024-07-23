using MediatR;
using Pame.Domain;

namespace Pame.Application;

public record ShowAllPayablesForStatus (Status Status) : IRequest<Response>;
