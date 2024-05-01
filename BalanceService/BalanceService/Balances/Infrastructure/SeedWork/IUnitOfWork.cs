namespace BalanceService.Balances.Infrastructure.SeedWork;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Saves the entities asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}