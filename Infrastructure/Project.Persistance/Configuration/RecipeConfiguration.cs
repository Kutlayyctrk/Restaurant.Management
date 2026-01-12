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
    public class RecipeConfiguration : BaseConfiguration<Recipe>
    {
        public override void Configure(EntityTypeBuilder<Recipe> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Product).WithOne(x => x.Recipe).HasForeignKey<Recipe>(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);//Bir ürünü silince reçetesinide siliyoruz.

            builder.HasOne(x => x.Category).WithMany(x => x.Recipes).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);//Bir kategoride reçete varsa kategoriyi sildirmiyoruz

            builder.HasMany(x => x.RecipeItems).WithOne(x => x.Recipe).HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);//Bir ürün reçeteye girdiyse onu sildirmiyoruz.
        }
    }
}
