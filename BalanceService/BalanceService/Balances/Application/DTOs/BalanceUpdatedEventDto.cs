using System.Text.Json.Serialization;

namespace BalanceService.Balances.Application.DTOs;

public class BalanceUpdatedEventDto
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Payload")]
    public Payload Payload { get; set; }
}

public class Payload
{
    public string account_id_from { get; set; }
    public string account_id_to { get; set; }
    public int balance_account_id_from { get; set; }
    public int balance_account_id_to { get; set; }
}