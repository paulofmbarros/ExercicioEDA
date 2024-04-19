using BalanceService.Balances.Domain;
using BalanceService.Balances.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Balances.Infrastructure.Cross_Cutting;

public class BalancesContext(IConfiguration configuration) : DbContext
{
    protected readonly IConfiguration Configuration = configuration;

    public DbSet<Balance> Balances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to mysql with connection string from app settings
        var connectionString = Configuration.GetConnectionString("WebApiDatabase");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BalancesEntityTypeConfigurations<Balance>());
    }
}