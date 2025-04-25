using Luftborn.Core.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luftborn.Infrastructure.Presistance.Data.EntityConfiguration;

internal sealed class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "Product");

        builder.Property(x => x.Name)
            .HasColumnType("nvarchar(128)")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);
    }
}