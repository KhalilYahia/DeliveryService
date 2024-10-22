using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Model.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Define the table name
            builder.ToTable("Order");

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            // Define properties

            builder.Property(o => o.OrderNumber)
               .IsRequired()
                .HasMaxLength(50);
            // Set unique constraint on OrderNumber
            builder.HasIndex(o => o.OrderNumber).IsUnique();


            builder.Property(o => o.Weight)
                .IsRequired();

            builder.Property(o => o.Region)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Coordinates)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.DeliveryTime)
                .IsRequired();


        }
    }
}
