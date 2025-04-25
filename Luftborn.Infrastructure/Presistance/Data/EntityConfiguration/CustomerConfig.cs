using Luftborn.Core.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luftborn.Infrastructure.Presistance.Data.EntityConfiguration;

internal sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "Customer");

        builder.Property(x => x.FirstName)
            .HasColumnType("nvarchar(64)")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnType("nvarchar(64)")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnType("nvarchar(64)")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasColumnType("nvarchar(32)")
            .HasMaxLength(32);

        builder.Property(x => x.Address)
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);
    }
}