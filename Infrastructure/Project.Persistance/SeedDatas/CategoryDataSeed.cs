using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class CategoryDataSeed
    {
        public static void CategorySeed(this ModelBuilder modelBuilder)
        {
            List<Category> categories = new()
            {
                //Ana Kategoriler
                new Category
                {
                    Id=1,
                    CategoryName="Yiyecekler",
                    Description="Ana Gıda Kategorisi",
                    ParentCategoryId=null,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=2,
                    CategoryName="İçecekler",
                    Description="Ana İçecek Kategorisi",
                    ParentCategoryId=null,
                   InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=3,
                    CategoryName="Sarf Malzemeleri",
                    Description="Ana Sarf Malzemeleri Kategorisi",
                    ParentCategoryId=null,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=4,
                    CategoryName="DemirBaş",
                    Description="Ana DemirBaş Kategorisi",
                    ParentCategoryId=null,
                   InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },

                //Alt Kategoriler
                new Category
                {
                    Id=5,
                    CategoryName="Etler",
                    Description="Et Ürünleri Kategorisi",
                    ParentCategoryId=1,
                   InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=6,
                    CategoryName="Sebzeler",
                    Description="Sebze Ürünleri Kategorisi",
                    ParentCategoryId=1,
                   InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=7,
                    CategoryName="Meyveler",
                    Description="Meyve Ürünleri Kategorisi",
                    ParentCategoryId=1,
                  InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=8,
                    CategoryName="Alkollü İçecekler",
                    Description="Alkollü İçecek Kategorisi",
                    ParentCategoryId=2,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=9,
                    CategoryName="Alkolsüz İçecekler",
                    Description="Alkolsüz İçecek Kategorisi",
                    ParentCategoryId=2,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted

                },
                new Category
                {
                    Id=10,
                    CategoryName="Temizlik Malzemeleri",
                    Description="Temizlik Malzemeleri Kategorisi",
                    ParentCategoryId=3,
                 InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=11,
                    CategoryName="Ofis Malzemeleri",
                    Description="Ofis Malzemeleri Kategorisi",
                    ParentCategoryId=3,
                   InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=12,
                    CategoryName="Mutfak Ekipmanları",
                    Description="Mutfak Ekipmanları Kategorisi",
                    ParentCategoryId=4,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Category
                {
                    Id=13,
                    CategoryName="Mobilyalar",
                    Description="Mobilya Kategorisi",
                    ParentCategoryId=4,
                  InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                }

            };
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
