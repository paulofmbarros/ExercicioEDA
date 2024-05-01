namespace BalanceService.Balances.Infrastructure.SeedWork;

public interface IRepository<TEntity>
{
    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    /// <value>The unit of work.</value>
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity);

}