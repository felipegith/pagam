using MediatR;

namespace Pame.Application;

public record ShowAllPayablesQuery : IRequest<Response>
{
    public static implicit operator ShowAllPayablesQuery(ShowAllPayablesQueryHandle v)
    {
        throw new NotImplementedException();
    }
}


