using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using Xunit;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.InnerInfrastructure.ManagerConcretes;
using Project.Persistance.ContextClasses;

namespace Project.UnitTests
{
    public class OrderManagerTests
    {
        [Fact]
        public async Task CreateAsync_WhenOrderDetailsEmpty_ReturnsFailed()
        {

            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object, Mock.Of<ILogger<OrderManager>>());

            OrderDTO dto = new OrderDTO
            {
                OrderDetails = new List<OrderDetailDTO>()
            };


            Result result = await manager.CreateAsync(dto);


            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_WhenValidationFails_ReturnsFailed_AndDoesNotPersist()
        {
            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            List<ValidationFailure> failures = new List<ValidationFailure>
            {
                new ValidationFailure("Type", "Type is required")
            };

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult(failures));

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object, Mock.Of<ILogger<OrderManager>>());

            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 1, UnitPrice = 10m }
                }
            };

            Result result = await manager.CreateAsync(dto);

            result.IsSuccess.Should().BeFalse();
            orderRepoMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Never);
            uowMock.Verify(u => u.CommitAsync(default), Times.Never);
            stockMock.Verify(s => s.CreateInitialOrderActionAsync(It.IsAny<OrderDetail>(), It.IsAny<OrderType>()), Times.Never);
            productMock.Verify(p => p.IncreaseStockAsync(It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_WithOrderDetails_PersistsAndCreatesStockActions()
        {

            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));


            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 5, UnitPrice = 10m }
                }
            };


            Order mappedOrder = new Order
            {
                Id = 0,
                Type = dto.Type,
                OrderDetails = dto.OrderDetails
                    .Select(d => new OrderDetail { Id = 0, ProductId = d.ProductId, Quantity = d.Quantity, UnitPrice = d.UnitPrice })
                    .ToList()
            };

            mapperMock
                .Setup(m => m.Map<Order>(It.IsAny<OrderDTO>()))
                .Returns(mappedOrder);


            DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            using MyContext context = new MyContext(options);


            context.Orders.RemoveRange(context.Orders);
            await context.SaveChangesAsync();


            orderRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Returns<Order>(async order =>
                {
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();
                });


            orderRepoMock
                .Setup(r => r.GetQuery())
                .Returns(() => context.Orders.AsQueryable());

            uowMock
                .Setup(u => u.CommitAsync(default))
                .ReturnsAsync(1);

            stockMock
                .Setup(s => s.CreateInitialOrderActionAsync(It.IsAny<OrderDetail>(), It.IsAny<OrderType>()))
                .Returns(Task.CompletedTask);

            productMock
                .Setup(p => p.IncreaseStockAsync(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object, Mock.Of<ILogger<OrderManager>>());


            Result result = await manager.CreateAsync(dto);


            result.IsSuccess.Should().BeTrue();


            orderRepoMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Once);


            uowMock.Verify(u => u.CommitAsync(default), Times.AtLeastOnce);


            stockMock.Verify(s => s.CreateInitialOrderActionAsync(It.Is<OrderDetail>(od => od.ProductId == 1 && od.Quantity == 5m), OrderType.Purchase), Times.Once);


            productMock.Verify(p => p.IncreaseStockAsync(1, 5m), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenCommitFails_ReturnsFailed()
        {
            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 5, UnitPrice = 10m }
                }
            };

            Order mappedOrder = new Order
            {
                Id = 0,
                Type = dto.Type,
                OrderDetails = dto.OrderDetails
                    .Select(d => new OrderDetail { Id = 0, ProductId = d.ProductId, Quantity = d.Quantity, UnitPrice = d.UnitPrice })
                    .ToList()
            };

            mapperMock
                .Setup(m => m.Map<Order>(It.IsAny<OrderDTO>()))
                .Returns(mappedOrder);
            orderRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Returns<Order>(async order => { });

            DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using MyContext context = new MyContext(options);

            context.Orders.RemoveRange(context.Orders);
            await context.SaveChangesAsync();


            orderRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Returns<Order>(async order =>
                {
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();
                });


            orderRepoMock
                .Setup(r => r.GetQuery())
                .Returns(() => context.Orders.AsQueryable());

            uowMock
                .Setup(u => u.CommitAsync(default))
                .ReturnsAsync(0);

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object, Mock.Of<ILogger<OrderManager>>());

            Result result = await manager.CreateAsync(dto);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_WhenDetailRemoved_CreatesDeletionStockActionAndUpdatesRepository()
        {
            // Hazırlık
            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));


            Order originalOrder = new Order
            {
                Id = 300,
                Type = OrderType.Sale,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail { Id = 10, ProductId = 2, Quantity = 3, UnitPrice = 5m }
                }
            };


            OrderDTO originalDto = new OrderDTO { Id = originalOrder.Id };
            OrderDTO newDto = new OrderDTO
            {
                Id = originalOrder.Id,
                OrderDetails = new List<OrderDetailDTO>()
            };


            DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            using MyContext context = new MyContext(options);


            context.Orders.Add(originalOrder);
            await context.SaveChangesAsync();

            orderRepoMock
                .Setup(r => r.GetQuery())
                .Returns(() => context.Orders.AsQueryable());


            orderRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            uowMock
                .Setup(u => u.CommitAsync(default))
                .ReturnsAsync(1);

            stockMock
                .Setup(s => s.CreateDeletionOrderActionAsync(It.IsAny<OrderDetail>(), It.IsAny<OrderType>()))
                .Returns(Task.CompletedTask);

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object, Mock.Of<ILogger<OrderManager>>());


            Result result = await manager.UpdateAsync(originalDto, newDto);


            result.IsSuccess.Should().BeTrue();


            stockMock.Verify(s => s.CreateDeletionOrderActionAsync(It.Is<OrderDetail>(od => od.Id == 10 && od.ProductId == 2), originalOrder.Type), Times.Once);


            orderRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Once);


            uowMock.Verify(u => u.CommitAsync(default), Times.AtLeastOnce);
        }

        [Fact]
        public async Task UpdateAsync_WhenValidationFails_ReturnsFailed_AndDoesNotUpdate()
        {
            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            List<ValidationFailure> failures = new List<ValidationFailure>
            {
                new ValidationFailure("OrderDetails", "Order details invalid")
            };

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult(failures));

            OrderDTO oldDto = new OrderDTO { Id = 1 };
            OrderDTO newDto = new OrderDTO
            {
                Id = 1,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 1, UnitPrice = 10m }
                }
            };

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object, Mock.Of<ILogger<OrderManager>>());

            Result result = await manager.UpdateAsync(oldDto, newDto);

            result.IsSuccess.Should().BeFalse();
            orderRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Never);
            uowMock.Verify(u => u.CommitAsync(default), Times.Never);
            stockMock.Verify(s => s.CreateDeletionOrderActionAsync(It.IsAny<OrderDetail>(), It.IsAny<OrderType>()), Times.Never);
        }
    }

}