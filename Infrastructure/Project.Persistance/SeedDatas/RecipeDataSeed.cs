using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class RecipeDataSeed
    {
        public static void RecipeSeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 14);

            List<Recipe> recipes = new()
            {
                new Recipe
                {
                    Id = 201,
                    ProductId = 1,
                    CategoryId = 5, 
                    Name = "Dana Bonfile Reçetesi",
                    Description = "Dana bonfile tabağı için temel reçete",
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Recipe
                {
                    Id = 202,
                    ProductId = 2,
                    CategoryId = 5,
                    Name = "Tavuk Izgara Reçetesi",
                    Description = "Tavuk ızgara tabağı için temel reçete",
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                }
            };

            modelBuilder.Entity<Recipe>().HasData(recipes);
        }
    }
}