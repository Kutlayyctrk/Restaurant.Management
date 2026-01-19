using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class RecipeItemDataSeed
    {
        public static void RecipeItemSeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 14);

            List<RecipeItem> items = new()
            {
                // Dana Bonfile Tabağı
                new RecipeItem
                {
                    Id = 301,
                    RecipeId = 201,
                    ProductId = 1,  // Dana Bonfile
                    Quantity = 1,
                    UnitId = 1,     // Kilogram
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new RecipeItem
                {
                    Id = 302,
                    RecipeId = 201,
                    ProductId = 3,  // Patates
                    Quantity = 150,
                    UnitId = 2,     // Adet
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new RecipeItem
                {
                    Id = 303,
                    RecipeId = 201,
                    ProductId = 4,  // Domates
                    Quantity = 50,
                    UnitId = 2,     // Adet
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },

                // Tavuk Izgara Tabağı
                new RecipeItem
                {
                    Id = 304,
                    RecipeId = 202,
                    ProductId = 2,  // Tavuk Izgara
                    Quantity = 1,
                    UnitId = 1,     // Kilogram
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new RecipeItem
                {
                    Id = 305,
                    RecipeId = 202,
                    ProductId = 3,  // Patates
                    Quantity = 100,
                    UnitId = 2,     // Adet
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new RecipeItem
                {
                    Id = 306,
                    RecipeId = 202,
                    ProductId = 4,  // Domates
                    Quantity = 30,
                    UnitId = 2,     // Adet
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                }
            };

            modelBuilder.Entity<RecipeItem>().HasData(items);
        }
    }
}