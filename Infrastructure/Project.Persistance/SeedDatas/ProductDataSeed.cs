using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class ProductDataSeed
    {
        public static void ProductSeed(this ModelBuilder modelBuilder)
        {
            List<Product> products = new()
           {
               new Product
               {
                   Id=1,
                   ProductName="Dana Bonfile",
                   IsSellable=true,
                   CanBeProduced=false,
                   UnitPrice=500,
                   UnitId=1,
                   CategoryId=5,
                   InsertedDate=DateTime.Now,
                   Status=Domain.Enums.DataStatus.Inserted

               },
               new Product
               {
                  Id=2,
                  ProductName="Tavuk Göğsü",
                  IsSellable=true,
                  CanBeProduced=false,
                  UnitPrice=150,
                  UnitId=1,
                  CategoryId=5,
                  InsertedDate=DateTime.Now,
                  Status=Domain.Enums.DataStatus.Inserted
               },
               new Product
               {
                  Id=3,
                  ProductName="Marul",
                  IsSellable=true,
                  CanBeProduced=false,
                  UnitPrice=20,
                  UnitId=2,
                  CategoryId=6,
                  InsertedDate=DateTime.Now,
                  Status=Domain.Enums.DataStatus.Inserted
               },
               new Product
               {
                  Id=4,
                  ProductName="Domates",
                  IsSellable=true,
                  CanBeProduced=false,
                  UnitPrice=25,
                  UnitId=1,
                  CategoryId=6,
                  InsertedDate=DateTime.Now,
                  Status=Domain.Enums.DataStatus.Inserted
               },
              new Product
              {
                Id=5,
                ProductName="Elma",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=30,
                UnitId=1,
                CategoryId=7,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
              },
             new Product
             {
                 Id=6,
                 ProductName="Muz",
                 IsSellable=true,
                 CanBeProduced=false,
                 UnitPrice=40,
                 UnitId=1,
                 CategoryId=7,
                 InsertedDate=new DateTime(2024,01,01),
                 Status=Domain.Enums.DataStatus.Inserted
             },


            new Product
            {
                Id=7,
                ProductName="Kola",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=15,
                UnitId=3,
                CategoryId=8,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=8,
                ProductName="Gazoz",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=12,
                UnitId=3,
                CategoryId=8,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },


            new Product
            {
                Id=9,
                ProductName="Ayran",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=10,
                UnitId=3,
                CategoryId=9,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=10,
                ProductName="Meyve Suyu",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=18,
                UnitId=3,
                CategoryId=9,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },


            new Product
            {
                Id=11,
                ProductName="Deterjan",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=60,
                UnitId=4,
                CategoryId=10,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=12,
                ProductName="Çamaşır Suyu",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=25,
                UnitId=4,
                CategoryId=10,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
             new Product
             {
                Id=13,
                ProductName="Kalem",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=2,
                UnitId=4,
                CategoryId=11,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
             },
            new Product
            {
                Id=14,
                ProductName="Defter",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=15,
                UnitId=2,
                CategoryId=11,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=15,
                ProductName="Tencere",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=200,
                UnitId=2,
                CategoryId=12,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=16,
                ProductName="Bıçak Seti",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=150,
                UnitId=2,
                CategoryId=12,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=17,
                ProductName="Sandalye",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=300,
                UnitId=2,
                CategoryId=13,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            },
            new Product
            {
                Id=18,
                ProductName="Masa",
                IsSellable=true,
                CanBeProduced=false,
                UnitPrice=700,
                UnitId=2,
                CategoryId=13,
                InsertedDate=new DateTime(2024,01,01),
                Status=Domain.Enums.DataStatus.Inserted
            }


           };
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
