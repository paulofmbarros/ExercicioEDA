using MediatR;

namespace BalanceService.Balances.Application.Commands.UpdateBalance;

public class UpdateBalanceCommand : IRequest<Unit>
{
    public string AccountIdFrom { get; set; }
    public string AccountIdTo { get; set; }
    public decimal BalanceAccountIdFrom { get; set; }
    public decimal BalanceAccountIdTo { get; set; }

}