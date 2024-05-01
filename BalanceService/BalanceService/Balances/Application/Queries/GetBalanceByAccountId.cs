using BalanceService.Balances.Domain;
using BalanceService.Balances.Infrastructure.Repositories;
using MediatR;

namespace BalanceService.Balances.Application.Queries;

public static class GetBalanceByAccountId
{
    public sealed record Query(Guid AccountId) : IRequest<Balance>;

    private sealed class QueryHandler(IBalanceRepository balanceRepository) : IRequestHandler<Query,Balance>
    {
        public async Task<Balance> Handle(Query request, CancellationToken cancellationToken)
        {
           var balance = await balanceRepository.GetByAccountId(request.AccountId);

           return balance;

        }
    }
}