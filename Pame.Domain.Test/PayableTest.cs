namespace Pame.Domain.Test;

public class PayableTest
{

    [Fact]
    public void Should_Be_Create_A_Object_Of_Type_Payable_And_Return_Object_Created()
    {
        string status = nameof(Status.Paid);
        DateTime paymentDate = DateTime.Now;
        decimal value = 1000;
        Method method = Method.Debit;

        var payable = Payable.Create(status, paymentDate, value, method);

        Assert.Equal(payable.PayableStatus, status);
        Assert.Equal(payable.PaymentDate.ToString("dd/MM/yyyy"), paymentDate.ToString("dd/MM/yyyy"));
    }
    [Fact]
    public void Should_Be_Payment_Date_Is_Date_Now_When_Method_Payment_Is_Debit()
    {
        var payable = new Payable();

        var paymentDate = payable.SetPaymentDate(Method.Debit);

        Assert.Equal(paymentDate.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
    }

    [Fact]
    public void Payable_Status_Should_Be_Paid_When_Transaction_Is_Debit()
    {
        var payable = new Payable();
        Method method = Method.Debit;
        var payableStatus = payable.SetPayableStatus(method);
        
        Assert.Equal(nameof(Status.Paid), payableStatus);
    }

    [Fact]
    public void Payable_Status_Should_Be_Waiting_Founds_When_Transaction_Is_Credit()
    {
        var payable = new Payable();
        Method method = Method.Credit;
        var payableStatus = payable.SetPayableStatus(method);
        
        Assert.Equal(nameof(Status.Waiting_Founds), payableStatus);
    }

    [Fact]
    public void Should_Return_Empty_String_When_Method_Payment_Is_Invalid()
    {
        var payable = new Payable();
        var payableStatus = payable.SetPayableStatus(0);
        
        Assert.Empty(payableStatus);
    }
    [Fact]
    public void Should_Be_Discount_Three_Percent_Value_When_Transaction_Is_Debit()
    {
        decimal value = 1000;
        Method method = Method.Debit;
        var payable = new Payable();
        var calculate = payable.CalculateRate(value, method);

        Assert.Equal(970, calculate);
    }

    [Fact]
    public void Should_Be_Discount_Five_Percent_Value_When_Transaction_Is_Credit()
    {
        decimal value = 1000;
        Method method = Method.Credit;
        var payable = new Payable();
        var calculate = payable.CalculateRate(value, method);

        Assert.Equal(950, calculate);
    }
}
