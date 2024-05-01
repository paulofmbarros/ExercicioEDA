using BalanceService.Balances.Infrastructure.Cross_Cutting;
using BalanceService.Balances.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Balances.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly BalancesContext Context;

    protected readonly DbSet<TEntity> _entities;

    public IUnitOfWork UnitOfWork => Context;

    public GenericRepository(BalancesContext context)
    {
        Context = context;
        _entities =  context.Set<TEntity>();;
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = await this._entities.AddAsync(entity);

        return entityEntry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var dataEntity = await Task.FromResult(this._entities.Update(entity));

        return dataEntity.Entity;
    }

}