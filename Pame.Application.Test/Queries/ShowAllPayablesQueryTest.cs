using System.Net;
using NSubstitute;
using Pame.Domain;

namespace Pame.Application.Test;

public class ShowAllPayablesQueryTest
{
    private static readonly ShowAllPayablesQuery Query = new();
    private readonly IPayableRepository _payableRepositoryMoq;
    private readonly ShowAllPayablesQueryHandle _handle;
    public ShowAllPayablesQueryTest()
    {
        _payableRepositoryMoq = Substitute.For<IPayableRepository>();
        _handle =  new ShowAllPayablesQueryHandle(_payableRepositoryMoq);
    }

    [Fact]
    public async Task Query_Should_Be_Return_All_Payables()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;


        var listOfPayables = new List<Payable>
        {
            Payable.Create(status, paymentDate, value, method),
            Payable.Create(status, paymentDate, value, method)
        };

        _payableRepositoryMoq.GetAllAsync(CancellationToken.None).Returns(listOfPayables);
        var result = await _handle.Handle(Query, default);
        Assert.Equal(listOfPayables, result.Content);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
