using Luftborn.Core.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luftborn.Infrastructure.Presistance.Data.EntityConfiguration;

internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "Product");
        builder.Property(x=>x.Name).HasColumnType("nvarchar(128)")
                                          .HasMaxLength(128).IsRequired();
        builder.Property(x => x.Description).
            HasColumnType("nvarchar(512)").HasMaxLength(512).IsRequired();
        builder.Property(x => x.ImageUrl).
            HasColumnType("nvarchar(512)").HasMaxLength(512);
        builder.Property(x=>x.StockQuantity).IsRequired();
    }
}