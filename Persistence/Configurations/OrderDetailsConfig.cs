using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.Property(ODs => ODs.ItemPrice).HasColumnType("money");
        builder.Property(ODs => ODs.TotalPrice).HasColumnType("money");
    }
}
