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
    public class RecipeItemConfiguration : BaseConfiguration<RecipeItem>
    {
        public override void Configure(EntityTypeBuilder<RecipeItem> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Quantity).HasPrecision(18, 2); //hassas miktarlar için

            builder.HasOne(x => x.Recipe).WithMany(x => x.RecipeItems).HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);// Bir reçete siliniyorsa ona baglı reçete Item'larıda silinir.
            builder.HasOne(x => x.Product).WithMany(x => x.RecipeItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);//Bir ürün bir reçeteye girdiyse silinemez
            builder.HasOne(x => x.Unit).WithMany(x => x.RecipeItems).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict);//Bir birim bir reçetede kullanıldıysa silinemez.
        }
    }
}
