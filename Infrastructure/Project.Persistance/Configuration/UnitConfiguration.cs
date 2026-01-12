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
    public class UnitConfiguration : BaseConfiguration<Unit>
    {
        public override void Configure(EntityTypeBuilder<Unit> builder)
        {
            base.Configure(builder);
            builder.HasMany(x => x.Products).WithOne(x => x.Unit).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict);//Bir birim bir product'da varsa silinemez
            builder.HasMany(x => x.RecipeItems).WithOne(x => x.Unit).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict);//Bir birim bir recipeitem'da varsa silinemez
        }
    }
}
