namespace Pame.Domain;

public class Payable
{
    public Guid Id {get; private set;}
    public string PayableStatus {get; private set;} = string.Empty;
    public DateTime PaymentDate{get; private set;}
    public decimal Value {get; private set;}


    public static Payable Create(string status, DateTime paymentDate, decimal value, Method method)
    {
       var payable =  new Payable 
        {
            Id = Guid.NewGuid(),
            PaymentDate = paymentDate,
            PayableStatus = status,
        };
        payable.Value = payable.CalculateRate(value, method);
        return payable;
    }

    public string SetPayableStatus(Method method)
    {
        switch (method)
        {
            case Method.Debit:
            return nameof(Status.Paid);

            case Method.Credit:
            return nameof(Status.Waiting_Founds);
            default:
            return string.Empty;
        }
    }

    public DateTime SetPaymentDate(Method method)
    {
        switch (method)
        {
            case Method.Debit:
            return DateTime.Now;

            case Method.Credit:
            return DateTime.Now.AddDays(30);
            default:
            return DateTime.MinValue;
        }
    }
    public decimal CalculateRate(decimal value, Method method)
    {
        decimal discount = 0;
        switch (method)
        {
            case Method.Debit:
            discount = value * 0.03m;
            return value - discount;

            case Method.Credit:
            discount = value * 0.05m;
            return value - discount;
            
            default:
            return 0;
        }
    }
    public string GetStatusPayable(Status status)
    {
        switch (status)
        {
            case Status.Paid:
            return nameof(Status.Paid);

            case Status.Waiting_Founds:
            return nameof(Status.Waiting_Founds);
            default:
            return string.Empty;
        }
    }
    
}
