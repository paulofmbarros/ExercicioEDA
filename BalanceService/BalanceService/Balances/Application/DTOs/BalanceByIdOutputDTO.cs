namespace BalanceService.Balances.Application.DTOs;

public class BalanceByIdOutputDto
{
    public int Id { get; init; }
    public Guid AccountId { get; init; }
    public decimal Amount { get; init; }
}