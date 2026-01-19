using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class CategoryDataSeed
    {
        public static void CategorySeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 14);

            List<Category> categories = new()
            {
                // Ana Kategoriler
                new Category
                {
                    Id = 1,
                    CategoryName = "Yiyecekler",
                    Description = "Ana Gıda Kategorisi",
                    ParentCategoryId = null,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "İçecekler",
                    Description = "Ana İçecek Kategorisi",
                    ParentCategoryId = null,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Sarf Malzemeleri",
                    Description = "Ana Sarf Malzemeleri Kategorisi",
                    ParentCategoryId = null,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 4,
                    CategoryName = "Demirbaş",
                    Description = "Ana Demirbaş Kategorisi",
                    ParentCategoryId = null,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },

                // Alt Kategoriler - Yiyecekler
                new Category
                {
                    Id = 5,
                    CategoryName = "Etler",
                    Description = "Et Ürünleri Kategorisi",
                    ParentCategoryId = 1,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },

                new Category
                {
                    Id = 6,
                    CategoryName = "Sebzeler",
                    Description = "Sebze Ürünleri Kategorisi",
                    ParentCategoryId = 1,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 7,
                    CategoryName = "Meyveler",
                    Description = "Meyve Ürünleri Kategorisi",
                    ParentCategoryId = 1,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },

                // Alt Kategoriler - İçecekler
                new Category
                {
                    Id = 8,
                    CategoryName = "Alkollü İçecekler",
                    Description = "Alkollü İçecek Kategorisi",
                    ParentCategoryId = 2,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 9,
                    CategoryName = "Alkolsüz İçecekler",
                    Description = "Alkolsüz İçecek Kategorisi",
                    ParentCategoryId = 2,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },

                // Alt Kategoriler - Sarf Malzemeleri
                new Category
                {
                    Id = 10,
                    CategoryName = "Temizlik Malzemeleri",
                    Description = "Temizlik Malzemeleri Kategorisi",
                    ParentCategoryId = 3,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 11,
                    CategoryName = "Ofis Malzemeleri",
                    Description = "Ofis Malzemeleri Kategorisi",
                    ParentCategoryId = 3,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },

                // Alt Kategoriler - Demirbaş
                new Category
                {
                    Id = 12,
                    CategoryName = "Mutfak Ekipmanları",
                    Description = "Mutfak Ekipmanları Kategorisi",
                    ParentCategoryId = 4,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id = 13,
                    CategoryName = "Mobilyalar",
                    Description = "Mobilya Kategorisi",
                    ParentCategoryId = 4,
                    InsertedDate = insertedDate,
                    Status = Project.Domain.Enums.DataStatus.Inserted
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}