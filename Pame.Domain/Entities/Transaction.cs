namespace Pame.Domain;

public class Transaction : Entity
{
    public Guid Id {get; private set;}
    public int Cvv {get; private set;}
    public Method Method {get; private set;}
    public decimal Value {get; private set;}
    public string Holder {get; private set;}
    public long CardNumber {get; private set;}
    public string Description {get; private set;}
    public DateTime ValidateCard {get; private set;}
    public static Transaction Create(string holder, string description, long cardNumber, DateTime validateCard, int cvv, decimal value, Method method)
    {
        
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Holder = holder,
            Description = description,
            ValidateCard = validateCard,
            Cvv = cvv,
            Value = value,
            Method = method
        };
        transaction.CardNumber = transaction.SaveLastFourNumberCard(cardNumber);
        return transaction;
    }

    public long SaveLastFourNumberCard(long cardNumber)
    {
       var preparate = cardNumber.ToString();
       var lastFourNumbers = preparate.Substring(preparate.Length - 4);

       return long.Parse(lastFourNumbers);
    }

}
