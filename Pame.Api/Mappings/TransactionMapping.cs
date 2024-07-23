using Mapster;
using Pame.Application;

namespace Pame.Api;

public class TransactionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransactionInput, MakeTransactionCommand>();
    }
}
