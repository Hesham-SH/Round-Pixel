using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(O => O.DiscountValue).HasColumnType("money");
        builder.Property(O => O.TotalPrice).HasColumnType("money");
        builder.Property(O => O.ExchangeRate).HasColumnType("money");
        builder.Property(O => O.ForeignPrice).HasColumnType("money");
    }
}
