using System.Net;
using NSubstitute;
using Pame.Domain;

namespace Pame.Application.Test;

public class ShowAllPayablesWithStatusPaidTest
{
    private static readonly ShowAllPayablesForStatus Query = new (Status.Paid);
    private readonly IPayableRepository _payableRepositoryMoq;
    private readonly ShowAllPayablesForStatusHandle _handle;

    public ShowAllPayablesWithStatusPaidTest()
    {
        _payableRepositoryMoq = Substitute.For<IPayableRepository>();
        _handle = new ShowAllPayablesForStatusHandle(_payableRepositoryMoq);
    }

    [Fact]
    public async Task Query_Should_Returned_All_Payables_With_Status_Paid_Or_Waiting_Funds()
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

        _payableRepositoryMoq.GetPayablesForStatus(status, CancellationToken.None).Returns(listOfPayables);
        var result = await _handle.Handle(Query, default);
        Assert.Equal(listOfPayables, result.Content);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
