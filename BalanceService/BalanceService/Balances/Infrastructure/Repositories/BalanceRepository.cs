using BalanceService.Balances.Domain;
using BalanceService.Balances.Infrastructure.Cross_Cutting;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Balances.Infrastructure.Repositories;

public class BalanceRepository(BalancesContext context) :  GenericRepository<Balance>(context), IBalanceRepository
{
    public async Task<Balance?> GetByAccountId(string accountId)
    {
        return await _entities.FirstOrDefaultAsync(x => x.AccountId == accountId);
    }
}