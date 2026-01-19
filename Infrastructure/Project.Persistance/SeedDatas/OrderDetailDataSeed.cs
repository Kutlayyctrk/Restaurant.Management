using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;

namespace Project.Persistance.SeedDatas
{
    public static class OrderDetailDataSeed
    {
        public static void OrderDetailSeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 18);

            List<OrderDetail> orderDetails = new()
            {
                // Order 1
                new OrderDetail
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1, // Dana Bonfile
                    Quantity = 1,
                    UnitPrice = 200m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new OrderDetail
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 9, // Coca Cola
                    Quantity = 1,
                    UnitPrice = 50m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },

                // Order 2
                new OrderDetail
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 2, // Tavuk Izgara
                    Quantity = 2,
                    UnitPrice = 90m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new OrderDetail
                {
                    Id = 4,
                    OrderId = 2,
                    ProductId = 7, // Bira
                    Quantity = 1,
                    UnitPrice = 50m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },

                // Order 3
                new OrderDetail
                {
                    Id = 5,
                    OrderId = 3,
                    ProductId = 11, // Izgara Köfte
                    Quantity = 1,
                    UnitPrice = 150m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new OrderDetail
                {
                    Id = 6,
                    OrderId = 3,
                    ProductId = 19, // Ayran
                    Quantity = 2,
                    UnitPrice = 15m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },

                // Order 4
                new OrderDetail
                {
                    Id = 7,
                    OrderId = 4,
                    ProductId = 20, // Gazoz
                    Quantity = 1,
                    UnitPrice = 18m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new OrderDetail
                {
                    Id = 8,
                    OrderId = 4,
                    ProductId = 15, // Portakal
                    Quantity = 1,
                    UnitPrice = 25m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                }
            };

            modelBuilder.Entity<OrderDetail>().HasData(orderDetails);
        }
    }
}