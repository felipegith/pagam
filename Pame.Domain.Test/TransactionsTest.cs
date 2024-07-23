namespace Pame.Domain.Test;

public class TransactionsTest
{
    
    [Fact]
    public void Should_Be_Create_A_Object_Of_Type_Transfer_And_Return_Object_Created()
    {
        string holder = "Felipe";
        decimal value = 204;
        string description = "Payment food";
        Method method = Method.Debit;
        long cardNumber = 3213139123231823;
        DateTime cardValidate = DateTime.Now.AddYears(2);
        int codeVerify = 324;

        var transaction = Transaction.Create(holder, description, cardNumber, cardValidate, codeVerify, value, method);

        Assert.NotNull(transaction);
        Assert.Equal(transaction.Holder, holder);
        Assert.Equal(transaction.Cvv, codeVerify);
        Assert.Equal(transaction.ValidateCard, cardValidate);
        Assert.Equal(transaction.Description, description);
        Assert.Equal(transaction.Value, value);
        Assert.Equal(transaction.Method, method);
    }

    [Fact]
    public void Should_Be_Get_Only_Last_Four_Number_Of_Card_Number()
    {
        long cardNumber = 3213139123231823;
        var transaction = new Transaction();
        var result = transaction.SaveLastFourNumberCard(cardNumber);
        
        Assert.Equal(1823, result);
        Assert.IsType<long>(result);
        Assert.True(result.ToString().Length == 4);
       
    }

    
}
