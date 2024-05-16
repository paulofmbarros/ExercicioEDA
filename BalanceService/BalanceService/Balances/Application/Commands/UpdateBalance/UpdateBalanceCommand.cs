using System.Text.Json.Serialization;
using MediatR;

namespace BalanceService.Balances.Application.Commands.UpdateBalance;

public class UpdateBalanceCommand : IRequest<Unit>
{
    [JsonPropertyName("account_id_from")]
    public string AccountIdFrom { get; set; }

    [JsonPropertyName("account_id_to")]
    public string AccountIdTo { get; set; }

    [JsonPropertyName("balance_account_id_from")]
    public decimal BalanceAccountIdFrom { get; set; }

    [JsonPropertyName("balance_account_id_to")]
    public decimal BalanceAccountIdTo { get; set; }

}