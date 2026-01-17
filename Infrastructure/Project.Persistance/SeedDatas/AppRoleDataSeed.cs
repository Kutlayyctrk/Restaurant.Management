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
            List<AppRole> roles = new()
            {
                new AppRole
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 3,
                    Name = "Insan Kaynaklari Muduru",
                    NormalizedName = "INSAN KAYNAKLARI MUDURU",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 2,
                    Name = "Restaurant Muduru",
                    NormalizedName = "RESTAURANT MUDURU",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 4,
                    Name = "Mutfak Sefi",
                    NormalizedName = "MUTFAK SEFI",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 5,
                    Name = "Bar Sefi",
                    NormalizedName = "BAR SEFI",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 6,
                    Name = "Asci",
                    NormalizedName = "ASCI",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 7,
                    Name = "Barmen",
                    NormalizedName = "BARMEN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 8,
                    Name = "Garson",
                    NormalizedName = "GARSON",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 9,
                    Name = "Hizmet Personeli",
                    NormalizedName = "HIZMET PERSONELI",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 10,
                    Name = "Idari Personel",
                    NormalizedName = "IDARI PERSONEL",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppRole
                {
                    Id = 11,
                    Name = "Stajyer",
                    NormalizedName = "STAJYER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                }
            };
            modelBuilder.Entity<AppRole>().HasData(roles);
        }
    }
}