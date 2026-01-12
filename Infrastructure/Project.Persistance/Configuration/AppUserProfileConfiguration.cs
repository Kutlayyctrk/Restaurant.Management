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
    public class AppUserProfileConfiguration : BaseConfiguration<AppUserProfile>
    {
        public override void Configure(EntityTypeBuilder<AppUserProfile> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.AppUser).WithOne(x => x.AppUserProfile).HasForeignKey<AppUserProfile>(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade); //Bir Appuser silindiğinde ona baglı AppUserProfile'ıda siliyoruz.
            builder.Property(x => x.Salary).HasColumnType("money");
        }
    }
}
