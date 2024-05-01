using BalanceService.Balances.Domain;
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
        var balanceFrom = await _balanceRepository.GetByAccountId(Guid.Parse( request.AccountIdFrom));

        if (balanceFrom is null)
        {
            var balance = new Balance(Guid.Parse(request.AccountIdFrom), request.BalanceAccountIdFrom);
            await this._balanceRepository.AddAsync(balance);
        }

        var balanceTo = await _balanceRepository.GetByAccountId(Guid.Parse(request.AccountIdTo));

        if (balanceTo is null)
        {
            var balance = new Balance(Guid.Parse(request.AccountIdTo), request.BalanceAccountIdTo);
            await this._balanceRepository.AddAsync(balance);
        }

        if(balanceFrom is not null && balanceTo is not null)
        {
            balanceFrom.UpdateAmount(request.BalanceAccountIdFrom);
            balanceTo.UpdateAmount(request.BalanceAccountIdTo);

            await _balanceRepository.UpdateAsync(balanceFrom);
            await _balanceRepository.UpdateAsync(balanceTo);
        }


        await this._balanceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Unit.Value;
    }
}