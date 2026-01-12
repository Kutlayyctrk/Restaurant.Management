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
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.TotalPrice).HasColumnType("money");
            builder.HasOne(x => x.Table).WithMany(x => x.Orders).HasForeignKey(x => x.TableId).OnDelete(DeleteBehavior.Restrict);//Masa silinse bile sipariş verilerini koruyoruz
            builder.HasOne(x => x.Waiter).WithMany(x => x.Orders).HasForeignKey(x => x.WaiterId).OnDelete(DeleteBehavior.Restrict);//Kullanıcı verisi silinse bile sipariş verilerini koruyoruz.
            builder.HasMany(x => x.OrderDetails).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);//Sipariş silinirse siparişdetaylarınıda kadameli siliyoruz.
        }
    }
}
