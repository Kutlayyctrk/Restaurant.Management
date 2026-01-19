using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;

namespace Project.Persistance.SeedDatas
{
    public static class OrderDataSeed
    {
        public static void OrderSeed(this ModelBuilder modelBuilder)
        {
            var insertedDate = new DateTime(2026, 01, 18);

            List<Order> orders = new()
            {
                new Order
                {
                    Id = 1,
                    TableId = 1,
                    WaiterId = 2,
                    TotalPrice = 250m,
                    OrderDate = new DateTime(2026,01,18,20,30,00),
                    OrderState = OrderStatus.SentToKitchen,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new Order
                {
                    Id = 2,
                    TableId = 2,
                    WaiterId = 3,
                    TotalPrice = 180m,
                    OrderDate = new DateTime(2026,01,18,21,00,00),
                    OrderState = OrderStatus.SentToKitchen,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new Order
                {
                    Id = 3,
                    TableId = 3,
                    WaiterId = 2,
                    TotalPrice = 320m,
                    OrderDate = new DateTime(2026,01,18,21,15,00),
                    OrderState = OrderStatus.SentToKitchen,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                },
                new Order
                {
                    Id = 4,
                    TableId = 4,
                    WaiterId = 4,
                    TotalPrice = 95m,
                    OrderDate = new DateTime(2026,01,18,21,30,00),
                    OrderState = OrderStatus.SentToKitchen,
                    InsertedDate = insertedDate,
                    Status = DataStatus.Inserted
                }
            };

            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}