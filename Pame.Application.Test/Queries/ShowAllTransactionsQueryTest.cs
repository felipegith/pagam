using MediatR;
using NSubstitute;
using Pame.Domain;

namespace Pame.Application.Test;


public class ShowAllTransactionsQueryTest
{
    private static readonly ShowAllTransactionsQuery Query = new();
    private readonly ITransactionRepository _transactionRepositoryMoq;
    private readonly ShowAllTransactionsQueryHandle _handle;
    public ShowAllTransactionsQueryTest()
    {
        _transactionRepositoryMoq = Substitute.For<ITransactionRepository>();
        _handle = new ShowAllTransactionsQueryHandle(_transactionRepositoryMoq);
    }

    [Fact]
    public async Task Query_Should_Be_Success_And_Return_All_Transactions()
    {
        string holder = "Felipe";
        decimal value = 204;
        string description = "Payment food";
        Method method = Method.Debit;
        long cardNumber = 3213139123231823;
        DateTime cardValidate = DateTime.Now.AddYears(2);
        int codeVerify = 324;

        var listTransactions = new List<Transaction>
        {
            Transaction.Create(holder, description, cardNumber, cardValidate, codeVerify, value, method),
            Transaction.Create(holder, description, cardNumber, cardValidate, codeVerify, value, method)
        };

        _transactionRepositoryMoq.GetAllAsync(CancellationToken.None).Returns(listTransactions);
        var result = await _handle.Handle(Query, default);
        Assert.Equal(listTransactions, result.Content);
    }

    
}   

