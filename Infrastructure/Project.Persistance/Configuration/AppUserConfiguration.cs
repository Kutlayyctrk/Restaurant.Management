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
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.AppUserProfile).WithOne(x => x.AppUser).HasForeignKey<AppUserProfile>(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade); //Bir Appuser silindiğinde ona baglı AppUserProfile'ıda siliyoruz.
            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();//Bir user siliniyorsa ona ait role kayıtlarıda silinir.
            builder.HasMany(x => x.Orders).WithOne(x => x.Waiter).HasForeignKey(x => x.WaiterId).OnDelete(DeleteBehavior.Restrict); //Bir AppUser silindiğinde ona ait sipariş verilerini sildirmiyruz.

        }
    }
}
