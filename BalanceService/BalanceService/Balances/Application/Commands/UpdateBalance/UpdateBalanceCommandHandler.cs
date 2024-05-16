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
        var accountFrom = await _balanceRepository.GetByAccountId(Guid.Parse( request.AccountIdFrom));

        if (accountFrom is null)
        {
            var balance = new Balance(Guid.Parse(request.AccountIdFrom), request.BalanceAccountIdFrom);
            await this._balanceRepository.AddAsync(balance);
        }

        var accountTo = await _balanceRepository.GetByAccountId(Guid.Parse(request.AccountIdTo));

        if (accountTo is null)
        {
            var balance = new Balance(Guid.Parse(request.AccountIdTo), request.BalanceAccountIdTo);
            await this._balanceRepository.AddAsync(balance);
        }

        if(accountFrom is not null && accountTo is not null)
        {
            accountFrom.UpdateAmount(request.BalanceAccountIdFrom);
            accountTo.UpdateAmount(request.BalanceAccountIdTo);

            await _balanceRepository.UpdateAsync(accountFrom);
            await _balanceRepository.UpdateAsync(accountTo);
        }


        await this._balanceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Unit.Value;
    }
}