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
    public class TableConfiguration : BaseConfiguration<Table>
    {
        public override void Configure(EntityTypeBuilder<Table> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Waiter).WithMany(x => x.Tables).HasForeignKey(x => x.WaiterId).OnDelete(DeleteBehavior.Restrict);//bir garson bir masaya bakıyorsa garson verisi silinemez
            builder.HasMany(x => x.Orders).WithOne(x => x.Table).HasForeignKey(x => x.TableId).OnDelete(DeleteBehavior.Restrict);//bir masada sipariş varsa o masa silinemez.
        }
    }
}
