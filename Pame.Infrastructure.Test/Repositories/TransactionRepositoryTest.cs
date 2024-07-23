using NSubstitute;
using Pame.Domain;

namespace Pame.Infrastructure.Test;

public class TransactionRepositoryTest
{
    private readonly ITransactionRepository _transactionRepositoryMoq;

    public TransactionRepositoryTest()
    {
        _transactionRepositoryMoq = Substitute.For<ITransactionRepository>();
    }
    [Fact]
    public void Repository_Should_Be_Add_Transaction_In_Database_And_Receive_Once()
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

        _transactionRepositoryMoq.Received(1).Create(transaction);

    }

    [Fact]
    public async Task Repository_Should_Be_Return_All_Transactions_In_Database()
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
        var transactions = await _transactionRepositoryMoq.GetAllAsync(CancellationToken.None);

        Assert.NotEmpty(transactions);
        Assert.IsType<List<Transaction>>(transactions);
    }
}
