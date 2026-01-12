using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.Configuration
{
    public class OrderDetailConfiguration : BaseConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new
            {
                x.OrderId,
                x.ProductId
            }).IsUnique();
            builder.Property(x => x.UnitPrice).HasColumnType("money");


            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade); //Bir sipariş silinirse sipariş detaylarınıda siliyoruz.
            builder.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict); //Bir ürün'de sipariş hareketi olursa sildirmiyoruz.
        }
    }
}
