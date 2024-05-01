using BalanceService.Balances.Domain;
using BalanceService.Balances.Infrastructure.EntityConfigurations;
using BalanceService.Balances.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Balances.Infrastructure.Cross_Cutting;

public class BalancesContext : DbContext, IUnitOfWork
{
    public DbSet<Balance> Balances { get; set; }

    public BalancesContext(DbContextOptions<BalancesContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BalancesEntityTypeConfigurations<Balance>());

        modelBuilder.Entity<Balance>().HasData(
            new Balance(1,Guid.NewGuid(), 100),
            new Balance( 2,Guid.NewGuid(), 200),
            new Balance( 3,Guid.NewGuid(), 300),
            new Balance( 4,Guid.NewGuid(), 400),
            new Balance( 5,Guid.NewGuid(), 500)
        );
    }
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await this.SaveChangesAsync(cancellationToken);

        return true;
    }

}