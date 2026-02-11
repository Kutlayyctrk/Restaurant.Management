using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Application.DTOs;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.InnerInfrastructure.ManagerConcretes;
using Xunit;

namespace Project.UnitTests
{
    public class StockTransActionManagerTests
    {
        private readonly Mock<IStockTransActionRepository> _repoMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<StockTransActionDTO>> _validatorMock;
        private readonly StockTransActionManager _manager;

        public StockTransActionManagerTests()
        {
            _repoMock = new Mock<IStockTransActionRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<StockTransActionDTO>>();

            _manager = new StockTransActionManager(
                _repoMock.Object,
                _uowMock.Object,
                _mapperMock.Object,
                _validatorMock.Object,
                Mock.Of<ILogger<StockTransActionManager>>());
        }

        #region CreateInitialOrderActionAsync

        [Fact]
        public async Task CreateInitialOrderActionAsync_Purchase_SetsTypeToPurchase()
        {
            OrderDetail detail = new OrderDetail { Id = 1, ProductId = 5, Quantity = 10, UnitPrice = 20m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateInitialOrderActionAsync(detail, OrderType.Purchase);

            captured.Should().NotBeNull();
            captured.Type.Should().Be(TransActionType.Purchase);
            captured.ProductId.Should().Be(5);
            captured.Quantity.Should().Be(10);
            captured.Status.Should().Be(DataStatus.Inserted);
        }

        [Fact]
        public async Task CreateInitialOrderActionAsync_Sale_SetsTypeToSale()
        {
            OrderDetail detail = new OrderDetail { Id = 2, ProductId = 3, Quantity = 5, UnitPrice = 15m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateInitialOrderActionAsync(detail, OrderType.Sale);

            captured.Type.Should().Be(TransActionType.Sale);
        }

        #endregion

        #region CreateDeletionOrderActionAsync

        [Fact]
        public async Task CreateDeletionOrderActionAsync_SetsTypeToReturnAndStatusToDeleted()
        {
            OrderDetail detail = new OrderDetail { Id = 10, ProductId = 2, Quantity = 3, UnitPrice = 5m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateDeletionOrderActionAsync(detail, OrderType.Purchase);

            captured.Should().NotBeNull();
            captured.Type.Should().Be(TransActionType.Return);
            captured.Status.Should().Be(DataStatus.Deleted);
            captured.ProductId.Should().Be(2);
            captured.Quantity.Should().Be(3);
            captured.Description.Should().Contain("Alým");
        }

        [Fact]
        public async Task CreateDeletionOrderActionAsync_Sale_DescriptionContainsSatýþ()
        {
            OrderDetail detail = new OrderDetail { Id = 10, ProductId = 2, Quantity = 3, UnitPrice = 5m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateDeletionOrderActionAsync(detail, OrderType.Sale);

            captured.Description.Should().Contain("Satýþ");
        }

        #endregion

        #region CreateUpdateOrderActionAsync

        [Fact]
        public async Task CreateUpdateOrderActionAsync_WhenDiffIsZero_DoesNothing()
        {
            OrderDetail detail = new OrderDetail { Id = 1, ProductId = 1, Quantity = 5, UnitPrice = 10m };

            await _manager.CreateUpdateOrderActionAsync(detail, 0, OrderType.Purchase);

            _repoMock.Verify(r => r.CreateAsync(It.IsAny<StockTransAction>()), Times.Never);
        }

        [Fact]
        public async Task CreateUpdateOrderActionAsync_Purchase_PositiveDiff_SetsPurchaseType()
        {
            OrderDetail detail = new OrderDetail { Id = 1, ProductId = 1, Quantity = 5, UnitPrice = 10m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateUpdateOrderActionAsync(detail, 3, OrderType.Purchase);

            captured.Type.Should().Be(TransActionType.Purchase);
            captured.Quantity.Should().Be(3);
        }

        [Fact]
        public async Task CreateUpdateOrderActionAsync_Purchase_NegativeDiff_SetsReturnType()
        {
            OrderDetail detail = new OrderDetail { Id = 1, ProductId = 1, Quantity = 5, UnitPrice = 10m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateUpdateOrderActionAsync(detail, -2, OrderType.Purchase);

            captured.Type.Should().Be(TransActionType.Return);
            captured.Quantity.Should().Be(2);
        }

        [Fact]
        public async Task CreateUpdateOrderActionAsync_Sale_PositiveDiff_SetsSaleType()
        {
            OrderDetail detail = new OrderDetail { Id = 1, ProductId = 1, Quantity = 5, UnitPrice = 10m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateUpdateOrderActionAsync(detail, 4, OrderType.Sale);

            captured.Type.Should().Be(TransActionType.Sale);
        }

        [Fact]
        public async Task CreateUpdateOrderActionAsync_Sale_NegativeDiff_SetsReturnType()
        {
            OrderDetail detail = new OrderDetail { Id = 1, ProductId = 1, Quantity = 5, UnitPrice = 10m };
            StockTransAction captured = null!;

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateUpdateOrderActionAsync(detail, -1, OrderType.Sale);

            captured.Type.Should().Be(TransActionType.Return);
            captured.Quantity.Should().Be(1);
        }

        #endregion

        #region CreateManualActionAsync

        [Fact]
        public async Task CreateManualActionAsync_MapsAndPersists()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 10,
                Type = TransActionType.Adjustment,
                Description = "Sayým farký"
            };

            StockTransAction entity = new StockTransAction
            {
                ProductId = 1,
                Quantity = 10,
                Type = TransActionType.Adjustment,
                Description = "Sayým farký"
            };

            StockTransAction captured = null!;

            _mapperMock.Setup(m => m.Map<StockTransAction>(dto)).Returns(entity);
            _repoMock.Setup(r => r.CreateAsync(It.IsAny<StockTransAction>()))
                .Callback<StockTransAction>(a => captured = a)
                .Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateManualActionAsync(dto);

            captured.Should().NotBeNull();
            captured.Status.Should().Be(DataStatus.Inserted);
            captured.InsertedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
            _repoMock.Verify(r => r.CreateAsync(It.IsAny<StockTransAction>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(default), Times.Once);
        }

        #endregion
    }
}
