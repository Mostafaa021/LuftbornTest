using Luftborn.Core.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luftborn.Infrastructure.Presistance.Data.EntityConfiguration;

internal sealed class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "Order");

        builder.Property(x => x.OrderDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>() // Store enum as string
            .HasMaxLength(32)
            .IsRequired();
    }
}