using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Project.Persistance.ContextClasses;
using Project.Persistance.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Xunit;

namespace Project.IntegrationTests
{
    public class OrderRepositoryIntegrationTests
    {
        [Fact]
        public async Task GetActiveOrderByTableIdAsync_ReturnsOrderWithDetailsAndProduct()
        {
            using (SqliteConnection connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                    .UseSqlite(connection)
                    .Options;

                int createdTableId;

                
                using (MyContext context = new MyContext(options))
                {
                    context.Database.EnsureCreated();

                    Unit unit = new Unit { UnitName = "adet", UnitAbbreviation = "ad" };
                    Category category = new Category { CategoryName = "TestCategory", Description = "Test category description" };
                    context.Units.Add(unit);
                    context.Categories.Add(category);
                    await context.SaveChangesAsync();

                    Product product = new Product
                    {
                        ProductName = "TestProduct",
                        UnitPrice = 12.5m,
                        CategoryId = category.Id,
                        UnitId = unit.Id,
                        Quantity = 0m
                    };
                    context.Products.Add(product);
                    await context.SaveChangesAsync();

                    Table table = new Table { TableNumber = "T42", TableStatus = TableStatus.Free };
                    context.Tables.Add(table);
                    await context.SaveChangesAsync();

                    Order order = new Order
                    {
                        TableId = table.Id,
                        OrderState = OrderStatus.SentToKitchen,
                        Type = OrderType.Sale,
                        OrderDate = DateTime.Now,
                        TotalPrice = 0m,
                        OrderDetails = new List<OrderDetail>
                        {
                            new OrderDetail { ProductId = product.Id, Quantity = 2, UnitPrice = product.UnitPrice }
                        }
                    };

                    context.Orders.Add(order);
                    await context.SaveChangesAsync();

                    createdTableId = table.Id;
                }

               
                using (MyContext context = new MyContext(options))
                {
                    OrderRepository repository = new OrderRepository(context);

                    Order result = await repository.GetActiveOrderByTableIdAsync(createdTableId);

                    result.Should().NotBeNull();
                    result.OrderDetails.Should().NotBeNull();
                    result.OrderDetails.Should().HaveCount(1);
                    result.OrderDetails.First().Product.Should().NotBeNull();
                    result.OrderDetails.First().Product.ProductName.Should().Be("TestProduct");
                }
            }

        }

        [Fact]
        public async Task GetActiveOrderByTableIdAsync_WhenNoOrderForTable_ReturnsNull()
        {
            using (SqliteConnection connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                    .UseSqlite(connection)
                    .Options;

                int createdTableId;

                using (MyContext context = new MyContext(options))
                {
                    context.Database.EnsureCreated();

                    Table table = new Table { TableNumber = "T100", TableStatus = TableStatus.Free };
                    context.Tables.Add(table);
                    await context.SaveChangesAsync();

                    createdTableId = table.Id;
                }

                using (MyContext context = new MyContext(options))
                {
                    OrderRepository repository = new OrderRepository(context);

                    Order result = await repository.GetActiveOrderByTableIdAsync(createdTableId);

                    result.Should().BeNull();
                }
            }
        }

        [Fact]
        public async Task GetActiveOrderByTableIdAsync_WithMultipleDetails_IncludesEachProduct()
        {
            using (SqliteConnection connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                    .UseSqlite(connection)
                    .Options;

                int createdTableId;

                using (MyContext context = new MyContext(options))
                {
                    context.Database.EnsureCreated();

                    Unit unit = new Unit { UnitName = "adet", UnitAbbreviation = "ad" };
                    Category category = new Category { CategoryName = "TestCategory", Description = "Test category description" };
                    context.Units.Add(unit);
                    context.Categories.Add(category);
                    await context.SaveChangesAsync();

                    Product product1 = new Product
                    {
                        ProductName = "P1",
                        UnitPrice = 10m,
                        CategoryId = category.Id,
                        UnitId = unit.Id,
                        Quantity = 0m
                    };

                    Product product2 = new Product
                    {
                        ProductName = "P2",
                        UnitPrice = 20m,
                        CategoryId = category.Id,
                        UnitId = unit.Id,
                        Quantity = 0m
                    };

                    context.Products.Add(product1);
                    context.Products.Add(product2);
                    await context.SaveChangesAsync();

                    Table table = new Table { TableNumber = "T200", TableStatus = TableStatus.Free };
                    context.Tables.Add(table);
                    await context.SaveChangesAsync();

                    Order order = new Order
                    {
                        TableId = table.Id,
                        OrderState = OrderStatus.SentToKitchen,
                        Type = OrderType.Sale,
                        OrderDate = DateTime.Now,
                        TotalPrice = 0m,
                        OrderDetails = new List<OrderDetail>
                        {
                            new OrderDetail { ProductId = product1.Id, Quantity = 1, UnitPrice = product1.UnitPrice },
                            new OrderDetail { ProductId = product2.Id, Quantity = 2, UnitPrice = product2.UnitPrice }
                        }
                    };

                    context.Orders.Add(order);
                    await context.SaveChangesAsync();

                    createdTableId = table.Id;
                }

                using (MyContext context = new MyContext(options))
                {
                    OrderRepository repository = new OrderRepository(context);

                    Order result = await repository.GetActiveOrderByTableIdAsync(createdTableId);

                    result.Should().NotBeNull();
                    result.OrderDetails.Should().HaveCount(2);
                    result.OrderDetails.Select(d => d.Product).Should().OnlyContain(p => p != null);
                    result.OrderDetails.Select(d => d.Product.ProductName).Should().BeEquivalentTo(new[] { "P1", "P2" });
                }
            }
        }
    }
}