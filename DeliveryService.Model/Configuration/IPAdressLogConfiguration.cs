using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Model.Configuration
{
    public class IPAdressLogConfiguration : IEntityTypeConfiguration<IPAdressLog>
    {
        
        public void Configure(EntityTypeBuilder<IPAdressLog> builder)
        {
            // Define the table name
            builder.ToTable("IPAdressLog");

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            // Define properties


            builder.Property(o => o.Ip)
                .IsRequired();


            builder.Property(o => o.Date)
                .IsRequired();


        }
    }
}
