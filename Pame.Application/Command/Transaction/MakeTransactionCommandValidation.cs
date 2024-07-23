using FluentValidation;

namespace Pame.Application;

public class MakeTransactionCommandValidation : AbstractValidator<MakeTransactionCommand>
{
    public MakeTransactionCommandValidation()
    {
        RuleFor(x=>x.Cvv).NotEmpty();
        RuleFor(x=>x.CardNumber).NotEmpty();
        RuleFor(x=>x.Holder).NotEmpty();
        RuleFor(x=>x.Description).NotEmpty();
        RuleFor(x=>x.Value).GreaterThan(0);
    }
}
