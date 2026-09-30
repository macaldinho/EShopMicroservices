using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany<OrderItem>()
            .WithOne()
            .HasForeignKey(o => o.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.ShippingAddress, nameBuilder =>
            {
                nameBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                nameBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                nameBuilder.Property(a => a.Country)
                .HasMaxLength(100);

                nameBuilder.Property(a => a.State)
                .HasMaxLength(50);

                nameBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.BillingAddress, nameBuilder =>
            {
                nameBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                nameBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                nameBuilder.Property(a => a.Country)
                .HasMaxLength(100);

                nameBuilder.Property(a => a.State)
                .HasMaxLength(50);

                nameBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.Payment, nameBuilder =>
            {
                nameBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

                nameBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                nameBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

                nameBuilder.Property(p => p.CVV)
                .HasMaxLength(3);

                nameBuilder.Property(p => p.PaymentMethod);
            });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}