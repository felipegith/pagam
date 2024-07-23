using NSubstitute;
using Pame.Domain;

namespace Pame.Infrastructure.Test;

public class PayableRepositoryTest
{
    private readonly IPayableRepository _payableRepositoryMoq;

    public PayableRepositoryTest()
    {
        _payableRepositoryMoq = Substitute.For<IPayableRepository>();
    }

    [Fact]
    public void Repository_Should_Be_Add_Payable_In_Database()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;

        var payable = Payable.Create(status, paymentDate, value, method);

        _payableRepositoryMoq.Create(payable);
        _payableRepositoryMoq.Received(1).Create(payable);
    }

    [Fact]
    public async Task Repository_Should_Be_Returned_Payables_When_Searching_Database_For_The_Customer_Name()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;
        var id = Guid.NewGuid();
         _payableRepositoryMoq.SearchPayableCostumers(id, default).Returns(Payable.Create(status, paymentDate, value, method));
        var costumerPayable = await _payableRepositoryMoq.SearchPayableCostumers(id, default);

        await _payableRepositoryMoq.Received(1).SearchPayableCostumers(id, default);
        Assert.NotEqual(Guid.Empty, costumerPayable.Id);
        Assert.Equal(status, costumerPayable.PayableStatus);
        Assert.Equal(paymentDate, costumerPayable.PaymentDate);
       
    }

    [Fact]
    public async Task Repository_Should_Returned_All_Payables_Of_DataBase()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;
        _payableRepositoryMoq.GetAllAsync(default).Returns(new List<Payable>{Payable.Create(status, paymentDate, value, method)});

        var payables = await _payableRepositoryMoq.GetAllAsync(default);

        Assert.NotEmpty(payables);
    }
    [Fact]
    public async Task Repository_Should_Return_All_Payables_With_Status_Paid()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;
        _payableRepositoryMoq.GetPayablesForStatus(status, default).Returns(new List<Payable>{Payable.Create(status, paymentDate, value, method), Payable.Create(status, paymentDate, value, method)});
        var payablesWithStatusPaid = await _payableRepositoryMoq.GetPayablesForStatus(status, default);
         
         
        await _payableRepositoryMoq.Received(1).GetPayablesForStatus(status,default);
        Assert.True(payablesWithStatusPaid.All(x=>x.PayableStatus == status));
    }

    [Fact]
    public async Task Repository_Should_Return_All_Payables_With_Status_Waiting_Funds()
    {
        string status = nameof(Status.Waiting_Founds);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Credit;
        _payableRepositoryMoq.GetPayablesForStatus(status, default).Returns(new List<Payable>{Payable.Create(status, paymentDate, value, method), Payable.Create(status, paymentDate, value, method)});
        var payablesWithStatusPaid = await _payableRepositoryMoq.GetPayablesForStatus(status, default);
         
         
        await _payableRepositoryMoq.Received(1).GetPayablesForStatus(status,default);
        Assert.True(payablesWithStatusPaid.All(x=>x.PayableStatus == status));
    }
    
}
