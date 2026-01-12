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
    public class StockTransActionConfiguration : BaseConfiguration<StockTransAction>
    {
        public override void Configure(EntityTypeBuilder<StockTransAction> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Quantity).HasPrecision(18, 2);
            builder.HasOne(x => x.Product).WithMany(x => x.StockTransActions).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);//Bir ürün transaction gördüyse silinemez
            builder.HasOne(x => x.Supplier).WithMany(x => x.StockTransActions).HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.Restrict);//bir tedarikçi transaction gördüyse silinemez.
            builder.HasOne(x => x.User).WithMany(x => x.StockTransActions).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);//bir kullanıcı transaction gördüyse silinemez.

        }
    }
}
