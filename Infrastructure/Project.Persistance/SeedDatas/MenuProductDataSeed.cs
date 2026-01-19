using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class MenuProductDataSeed
    {
        public static void MenuProductSeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 14);

            List<MenuProduct> menuProducts = new()
            {
              new MenuProduct
                {
                    Id = 101,
                    MenuId = 1, // 2026 Kış Menüsü
                    ProductId = 1, // Dana Bonfile
                    IsActive = true,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new MenuProduct
                {
                    Id = 102,
                    MenuId = 1,
                    ProductId = 2, // Tavuk Izgara
                    IsActive = true,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new MenuProduct
                {
                    Id = 103,
                    MenuId = 1,
                    ProductId = 9, // Coca Cola (doğru Id)
                    IsActive = true,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new MenuProduct
                {
                    Id = 104,
                    MenuId = 1,
                    ProductId = 7, // Bira (doğru Id)
                    IsActive = true,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new MenuProduct
                {
                    Id = 105,
                    MenuId = 1,
                    ProductId = 19, // Ayran (doğru Id)
                    IsActive = true,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                }       
            };

            modelBuilder.Entity<MenuProduct>().HasData(menuProducts);
        }
    }
}