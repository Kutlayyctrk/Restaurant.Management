using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class MenuSeedData
    {
        public static void MenuSeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 14);

            List<Menu> menus = new()
            {
                new Menu
                {
                    Id = 1,
                    MenuName = "2026 Kış Menüsü",
                    StartDate = new DateTime(2026, 01, 01),
                    EndDate = new DateTime(2026, 03, 31),
                    IsActive = true,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new Menu
                {
                    Id = 2,
                    MenuName = "2026 Yaz Menüsü",
                    StartDate = new DateTime(2026, 06, 01),
                    EndDate = new DateTime(2026, 08, 31),
                    IsActive = false,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                }
            };

            modelBuilder.Entity<Menu>().HasData(menus);
        }
    }
}