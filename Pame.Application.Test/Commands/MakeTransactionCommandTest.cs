using System.Net;
using NSubstitute;
using Pame.Domain;

namespace Pame.Application.Test;

public class MakeTransactionCommandTest
{
    private static readonly MakeTransactionCommand Command = new ("Felipe", 204, "Payment food", Method.Debit, 3213139123231823, DateTime.Now.AddYears(2), 324);
    private readonly MakeTransactionCommandHandle _handle;

    private readonly ITransactionRepository _transactionRepositoryMoq;
    private readonly IUnitOfWork _uowMoq;

    public MakeTransactionCommandTest()
    {
        _transactionRepositoryMoq = Substitute.For<ITransactionRepository>();
        _uowMoq = Substitute.For<IUnitOfWork>();
        _handle = new MakeTransactionCommandHandle(_transactionRepositoryMoq, _uowMoq);
    }
    [Fact]
    public async Task Handle_Should_Be_Make_A_Transaction_Sucessfully()
    {
        string holder = "Felipe";
        decimal value = 204;
        string description = "Payment food";
        Method method = Method.Debit;
        long cardNumber = 3213139123231823;
        DateTime cardValidate = DateTime.Now.AddYears(2);
        int codeVerify = 324;

        var transaction = Transaction.Create(holder, description, cardNumber, cardValidate, codeVerify, value, method);

        _transactionRepositoryMoq.Create(transaction);
        var result = await _handle.Handle(Command, default);

        _transactionRepositoryMoq.Received(1).Create(transaction);

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        Assert.True(result.Success);
    }
   
}
