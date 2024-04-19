using BalanceService.Balances.Infrastructure.Repositories;
using MediatR;

namespace BalanceService.Balances.Application.Commands.UpdateBalance;


public class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommand, Unit>
{
    private readonly IBalanceRepository _balanceRepository;

    public UpdateBalanceCommandHandler(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<Unit> Handle(UpdateBalanceCommand request, CancellationToken cancellationToken)
    {
        var balanceFrom = await _balanceRepository.GetByAccountId(request.AccountIdFrom);
        var balanceTo = await _balanceRepository.GetByAccountId(request.AccountIdTo);

        if (balanceFrom == null || balanceTo == null)
        {
            throw new Exception("Balance not found");
        }

        balanceFrom.UpdateAmount(request.BalanceAccountIdFrom);
        balanceTo.UpdateAmount(request.BalanceAccountIdTo);

        await _balanceRepository.UpdateAsync(balanceFrom);
        await _balanceRepository.UpdateAsync(balanceTo);

        return Unit.Value;
    }
}