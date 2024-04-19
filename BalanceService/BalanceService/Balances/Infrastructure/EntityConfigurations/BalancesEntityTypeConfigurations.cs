using BalanceService.Balances.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceService.Balances.Infrastructure.EntityConfigurations;

public class BalancesEntityTypeConfigurations<TEntity> : IEntityTypeConfiguration<Balance>
{
    public string TableName => "Balances";

    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.ToTable(this.TableName);

        builder.HasKey(t => t.Id);

        builder.Property(t => t.AccountId)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(t => t.Amount)
            .IsRequired();
    }
}