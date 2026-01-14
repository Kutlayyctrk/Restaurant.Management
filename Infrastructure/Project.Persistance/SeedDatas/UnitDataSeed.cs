using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class UnitDataSeed
    {
        public static void UnitSeed(this ModelBuilder modelBuilder)
        {
            List<Unit> unit = new()
            {
                new Unit()
                {
                    Id = 1,
                    UnitName = "Kilogram",
                    UnitAbbreviation = "kg",
                     InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted

                },
                new Unit()
                {
                    Id=2,
                    UnitName = "Adet",
                    UnitAbbreviation = "ad",
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted

                },
                new Unit()
                {
                    Id=3,
                    UnitName = "Litre",
                    UnitAbbreviation = "lt",
                     InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Unit()
                {
                    Id=4,
                    UnitName = "Paket",
                    UnitAbbreviation = "pkt",
                     InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                

            };
            modelBuilder.Entity<Unit>().HasData(unit);
        }
    }
}
