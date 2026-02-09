using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Project.Application.DTOs;
using Project.Application.Enums;
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
            // Hazırlık
            Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<IValidator<OrderDTO>> validatorMock = new Mock<IValidator<OrderDTO>>();
            Mock<IStockTransActionManager> stockMock = new Mock<IStockTransActionManager>();
            Mock<IProductManager> productMock = new Mock<IProductManager>();

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<OrderDTO>(), default))
                .ReturnsAsync(new ValidationResult());

            OrderManager manager = new OrderManager(
                orderRepoMock.Object,
                uowMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                stockMock.Object,
                productMock.Object);

            OrderDTO dto = new OrderDTO
            {
                OrderDetails = new List<OrderDetailDTO>() // boş -> metot Failure dönmeli
            };

            // İşlem
            OperationStatus result = await manager.CreateAsync(dto);

            // Doğrulama
            result.Should().Be(OperationStatus.Failed);
        }

        [Fact]
        public async Task CreateAsync_WithOrderDetails_PersistsAndCreatesStockActions()
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
                .ReturnsAsync(new ValidationResult());

            // Bir detaylı DTO
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 5, UnitPrice = 10m }
                }
            };

            // Mapper: OrderDTO'yu Order'a map et ve OrderDetail'leri oluştur
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

            // FirstOrDefaultAsync çalışsın diye EF Core InMemory DbContext kullan
            DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: "OrderManagerTests_Db")
                .Options;

            using MyContext context = new MyContext(options);

            // Başlangıçta veritabanını temizle
            context.Orders.RemoveRange(context.Orders);
            await context.SaveChangesAsync();

            // CreateAsync'i in-memory context'e kaydedecek şekilde mock'la
            orderRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Returns<Order>(async order =>
                {
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();
                });

            // FirstOrDefaultAsync'in çalışması için gerçek DbSet<Order> sorgulanabilir nesnesi dön
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
                productMock.Object);

            // İşlem
            OperationStatus result = await manager.CreateAsync(dto);

            // Doğrulamalar
            result.Should().Be(OperationStatus.Success);

            // Repository'nin CreateAsync metodu bir kez çağrıldı mı?
            orderRepoMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Once);

            // UnitOfWork.CommitAsync en az bir kez çağrıldı mı?
            uowMock.Verify(u => u.CommitAsync(default), Times.AtLeastOnce);

            // Persist edilen sipariş detayları için stok hareketi oluşturuldu mu?
            stockMock.Verify(s => s.CreateInitialOrderActionAsync(It.Is<OrderDetail>(od => od.ProductId == 1 && od.Quantity == 5m), OrderType.Purchase), Times.Once);

            // Alım siparişi olduğu için ürün stoğu arttırılmalı
            productMock.Verify(p => p.IncreaseStockAsync(1, 5m), Times.Once);
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
                .ReturnsAsync(new ValidationResult());

            // Orijinal sipariş (veritabanında var)
            Order originalOrder = new Order
            {
                Id = 300,
                Type = OrderType.Sale,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail { Id = 10, ProductId = 2, Quantity = 3, UnitPrice = 5m }
                }
            };

            // Yeni DTO: detaylar boş -> mevcut detay silinecek
            OrderDTO originalDto = new OrderDTO { Id = originalOrder.Id };
            OrderDTO newDto = new OrderDTO
            {
                Id = originalOrder.Id,
                OrderDetails = new List<OrderDetailDTO>() // tüm detaylar kaldırıldı
            };

            // InMemory DbContext hazırla (her test için izole DB adı kullan)
            DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            using MyContext context = new MyContext(options);

            // Orijinal siparişi DB'ye ekle ve kaydet
            context.Orders.Add(originalOrder);
            await context.SaveChangesAsync();

            // GetQuery() gerçek DbSet sorgulanabilir nesnesi dönsün
            orderRepoMock
                .Setup(r => r.GetQuery())
                .Returns(() => context.Orders.AsQueryable());

            // UpdateAsync repository çağrısını mock'la (gerçek güncelleme context üzerinden yapılmayacak)
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
                productMock.Object);

            // İşlem
            OperationStatus result = await manager.UpdateAsync(originalDto, newDto);

            // Doğrulamalar
            result.Should().Be(OperationStatus.Success);

            // Silinen detay için stok silme aksiyonu oluşturuldu mu?
            stockMock.Verify(s => s.CreateDeletionOrderActionAsync(It.Is<OrderDetail>(od => od.Id == 10 && od.ProductId == 2), originalOrder.Type), Times.Once);

            // Repository UpdateAsync çağrıldı mı?
            orderRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Once);

            // UnitOfWork Commit çağrıldı mı
            uowMock.Verify(u => u.CommitAsync(default), Times.AtLeastOnce);
        }
    }
}