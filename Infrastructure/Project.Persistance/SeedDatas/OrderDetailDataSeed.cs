using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;

namespace Project.Persistance.SeedDatas
{
    public static class OrderDetailDataSeed
    {
        public static void OrderDetailSeed(this ModelBuilder modelBuilder)
        {
            List<OrderDetail> orderDetails = new()
            {
                new OrderDetail
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 5,
                    Quantity = 1,
                    UnitPrice = 200m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = DateTime.Now,
                     Status= DataStatus.Inserted
                },
                new OrderDetail
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 8,
                    Quantity = 1,
                    UnitPrice = 50m,
                    DetailState = OrderDetailStatus.Waiting,
                    InsertedDate = DateTime.Now,
                    Status = DataStatus.Inserted
                },
                new OrderDetail
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 6,
                    Quantity = 2,
                    UnitPrice = 90m,
                    DetailState = OrderDetailStatus.Preparing,
                    InsertedDate = DateTime.Now,
                    Status = DataStatus.Inserted
                }
            };

            modelBuilder.Entity<OrderDetail>().HasData(orderDetails);
        }
    }
}