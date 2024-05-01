namespace BalanceService.Balances.Domain;

public class Balance
{
    public virtual int Id { get; private set; }
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }

    public Balance(int id, Guid accountId, decimal amount)
    {
        Id = id;
        AccountId = accountId;
        Amount = amount;
    }

    public Balance(Guid accountId, decimal amount)
    {
        AccountId = accountId;
        Amount = amount;
    }

    public Balance()
    {
    }



    public void UpdateAmount(decimal amount)
    {
        this.Amount = amount;
    }
}