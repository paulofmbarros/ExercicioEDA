using BalanceService.Balances.Domain;
using BalanceService.Balances.Infrastructure.SeedWork;

namespace BalanceService.Balances.Infrastructure.Repositories;

public interface IBalanceRepository : IRepository<Balance>
{
    Task<Balance?> GetByAccountId(string accountId);
}