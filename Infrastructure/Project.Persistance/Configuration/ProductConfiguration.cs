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
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.UnitPrice).HasColumnType("money");

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict); //Bir category'de ürün varsa sildirmiyoruz.

            builder.HasOne(x => x.Unit).WithMany(x => x.Products).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict); //Bir birim'de ürün varsa sildirmiyoruz

            builder.HasOne(x => x.Recipe).WithOne(x => x.Product).HasForeignKey<Recipe>(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);//Bir ürün silinirse reçetesinide siliyoruz.

            builder.HasMany(x => x.RecipeItems).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);//Bir ürün bir reçete'de varsa sildirmiyoruz.
            builder.HasMany(x => x.StockTransActions).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);//Bir ürün stock kaydına girdiyse sildirmiyoruz.
        }
    }
}
