using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class ProductDataSeed
    {
        public static void ProductSeed(this ModelBuilder modelBuilder)
        {
            List<Product> products = new()
            {
                // Etler
                new Product
                {
                    Id = 1,
                    ProductName = "Dana Bonfile",
                    CategoryId = 5,
                    UnitId = 1,
                    UnitPrice = 250,
                    IsSellable = true,
                    CanBeProduced = true,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Tavuk Izgara",
                    CategoryId = 5,
                    UnitId = 1,
                    UnitPrice = 180,
                    IsSellable = true,
                    CanBeProduced = true,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Sebzeler
                new Product
                {
                    Id = 3,
                    ProductName = "Patates",
                    CategoryId = 6,
                    UnitId = 2,
                    UnitPrice = 30,
                    IsSellable = false,
                    CanBeProduced = false,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 4,
                    ProductName = "Domates",
                    CategoryId = 6,
                    UnitId = 2,
                    UnitPrice = 25,
                    IsSellable = false,
                    CanBeProduced = false,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Meyveler
                new Product
                {
                    Id = 5,
                    ProductName = "Elma",
                    CategoryId = 7,
                    UnitId = 2,
                    UnitPrice = 20,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 6,
                    ProductName = "Muz",
                    CategoryId = 7,
                    UnitId = 2,
                    UnitPrice = 22,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Alkollü İçecekler
                new Product
                {
                    Id = 7,
                    ProductName = "Bira",
                    CategoryId = 8,
                    UnitId = 3,
                    UnitPrice = 60,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 8,
                    ProductName = "Şarap",
                    CategoryId = 8,
                    UnitId = 3,
                    UnitPrice = 120,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Alkolsüz İçecekler
                new Product
                {
                    Id = 9,
                    ProductName = "Coca Cola",
                    CategoryId = 9,
                    UnitId = 3,
                    UnitPrice = 35,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 10,
                    ProductName = "Soda",
                    CategoryId = 9,
                    UnitId = 3,
                    UnitPrice = 20,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 11,
                    ProductName = "Izgara Köfte",
                    CategoryId = 5,
                    UnitId = 1,
                    UnitPrice = 150,
                    IsSellable = true,
                    CanBeProduced = true,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 12,
                    ProductName = "Antrikot",
                    CategoryId = 5,
                    UnitId = 1,
                    UnitPrice = 200,
                    IsSellable = true,
                    CanBeProduced = true,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Sebzeler
                new Product
                {
                    Id = 13,
                    ProductName = "Soğan",
                    CategoryId = 6,
                    UnitId = 2,
                    UnitPrice = 15,
                    IsSellable = false,
                    CanBeProduced = false,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 14,
                    ProductName = "Biber",
                    CategoryId = 6,
                    UnitId = 2,
                    UnitPrice = 18,
                    IsSellable = false,
                    CanBeProduced = false,
                    IsReadyMade = false,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Meyveler
                new Product
                {
                    Id = 15,
                    ProductName = "Portakal",
                    CategoryId = 7,
                    UnitId = 2,
                    UnitPrice = 25,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 16,
                    ProductName = "Çilek",
                    CategoryId = 7,
                    UnitId = 2,
                    UnitPrice = 40,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Alkollü İçecekler
                new Product
                {
                    Id = 17,
                    ProductName = "Rakı",
                    CategoryId = 8,
                    UnitId = 3,
                    UnitPrice = 180,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 18,
                    ProductName = "Viski",
                    CategoryId = 8,
                    UnitId = 3,
                    UnitPrice = 250,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

                // Alkolsüz İçecekler
                new Product
                {
                    Id = 19,
                    ProductName = "Ayran",
                    CategoryId = 9,
                    UnitId = 3,
                    UnitPrice = 15,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new Product
                {
                    Id = 20,
                    ProductName = "Gazoz",
                    CategoryId = 9,
                    UnitId = 3,
                    UnitPrice = 18,
                    IsSellable = true,
                    CanBeProduced = false,
                    IsReadyMade = true,
                    IsExtra = false,
                    InsertedDate = new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                }

            };

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
