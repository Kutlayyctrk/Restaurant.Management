using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{

    public static class AppRoleDataSeed

    {
        public static void AppRoleSeed(this ModelBuilder modelBuilder)
        {

            List<AppRole> roles = new ()
        {
            new AppRole
            {
                Id=1,
                Name="Admin",
               NormalizedName="ADMIN",
               ConcurrencyStamp=Guid.NewGuid().ToString(),
               InsertedDate =new DateTime(2026,01,14),
                Status=Domain.Enums.DataStatus.Inserted

            },
            new AppRole
            {
                Id=3,
                Name="İnsan Kaynakları Müdürü",
                NormalizedName="İNSAN KAYNAKLARI MÜDÜRÜ",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=2,
                Name="Restaurant Müdürü",
                NormalizedName="RESTAURANT MÜDÜRÜ",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
               InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=4,
                Name="Mutfak Şefi",
                NormalizedName="MUTFAK ŞEFİ",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=5,
                Name="Bar Şefi",
                NormalizedName="BAR ŞEFİ",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=6,
                Name="Aşçı",
                NormalizedName="AŞÇI",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
               InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=7,
                Name="Barmen",
                NormalizedName="BARMEN",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
              InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=8,
                Name="garson",
                NormalizedName="GARSON",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=9,
                Name="Hizmet Personeli",
                NormalizedName="HİZMET PERSONELİ",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                 InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=10,
                Name="İdari Personel",
                NormalizedName="İDARİ PERSONEL",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
              InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            },
            new AppRole
            {
                Id=11,
                Name="Stajyer",
                NormalizedName="STAJYER",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                InsertedDate =new DateTime(2026,01,14),
                 Status=Domain.Enums.DataStatus.Inserted
            }

        };
            modelBuilder.Entity<AppRole>().HasData(roles);

        }



    }
}
