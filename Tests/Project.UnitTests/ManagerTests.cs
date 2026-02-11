using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.InnerInfrastructure.ManagerConcretes;
using Xunit;

namespace Project.UnitTests
{
    public class RecipeManagerTests
    {
        private readonly Mock<IRecipeRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<RecipeDTO>> _validatorMock = new();
        private readonly RecipeManager _manager;

        public RecipeManagerTests()
        {
            _manager = new RecipeManager(_repoMock.Object, _uowMock.Object, _mapperMock.Object, _validatorMock.Object, Mock.Of<ILogger<RecipeManager>>());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
            {
                new() { Id = 1, Name = "R1", Description = "D", ProductId = 1, CategoryId = 1, RecipeItems = new List<RecipeItem> { new() { ProductId = 1, Quantity = 2, UnitId = 1 } } }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(recipes);

            var result = await _manager.GetAllAsync();
            result.Should().HaveCount(1);
            result[0].Name.Should().Be("R1");
            result[0].RecipeItems.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdWithItemsAsync_WhenFound_ReturnsDto()
        {
            var recipe = new Recipe { Id = 1, Name = "R", Description = "D", ProductId = 1, CategoryId = 1, RecipeItems = new List<RecipeItem> { new() { ProductId = 2, Quantity = 3, UnitId = 1 } } };
            _repoMock.Setup(r => r.GetByIdWithItemsAsync(1)).ReturnsAsync(recipe);

            var result = await _manager.GetByIdWithItemsAsync(1);
            result.Should().NotBeNull();
            result!.RecipeItems.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdWithItemsAsync_WhenNotFound_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetByIdWithItemsAsync(99)).ReturnsAsync((Recipe?)null);
            var result = await _manager.GetByIdWithItemsAsync(99);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByProductIdAsync_WhenFound_ReturnsDto()
        {
            var recipe = new Recipe { Id = 1, Name = "R", Description = "D", ProductId = 5, CategoryId = 1, RecipeItems = new List<RecipeItem> { new() { ProductId = 1, Quantity = 1, UnitId = 1 } } };
            _repoMock.Setup(r => r.GetByProductIdAsync(5)).ReturnsAsync(recipe);

            var result = await _manager.GetByProductIdAsync(5);
            result.Should().NotBeNull();
            result!.ProductId.Should().Be(5);
        }

        [Fact]
        public async Task GetByProductIdAsync_WhenNotFound_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetByProductIdAsync(99)).ReturnsAsync((Recipe?)null);
            var result = await _manager.GetByProductIdAsync(99);
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_WhenFound_ReturnsSuccess()
        {
            var entity = new Recipe { Id = 1, Name = "Old", Status = DataStatus.Inserted };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Recipe>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            var result = await _manager.UpdateAsync(new RecipeDTO { Id = 1 }, new RecipeDTO { Id = 1, Name = "New" });
            result.IsSuccess.Should().BeTrue();
            entity.Status.Should().Be(DataStatus.Updated);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Recipe?)null);
            var result = await _manager.UpdateAsync(new RecipeDTO { Id = 99 }, new RecipeDTO { Id = 99 });
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.NotFound);
        }
    }

    public class MenuProductManagerTests
    {
        private readonly Mock<IMenuProductRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<MenuProductDTO>> _validatorMock = new();
        private readonly MenuProductManager _manager;

        public MenuProductManagerTests()
        {
            _manager = new MenuProductManager(_repoMock.Object, _uowMock.Object, _mapperMock.Object, _validatorMock.Object, Mock.Of<ILogger<MenuProductManager>>());
        }

        [Fact]
        public async Task GetWithMenuAndProduct_ReturnsList()
        {
            var entities = new List<MenuProduct> { new() { Id = 1, MenuId = 1, ProductId = 1, UnitPrice = 10m } };
            var dtos = new List<MenuProductDTO> { new() { Id = 1, MenuId = 1, ProductId = 1, UnitPrice = 10m } };
            _repoMock.Setup(r => r.GetWithMenuAndProduct()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<List<MenuProductDTO>>(entities)).Returns(dtos);

            var result = await _manager.GetWithMenuAndProduct();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateAsync_WhenFound_ReturnsSuccess()
        {
            var entity = new MenuProduct { Id = 1, MenuId = 1, ProductId = 1, UnitPrice = 5m, Status = DataStatus.Inserted };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<MenuProduct>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            var result = await _manager.UpdateAsync(new MenuProductDTO { Id = 1 }, new MenuProductDTO { Id = 1, UnitPrice = 20m });
            result.IsSuccess.Should().BeTrue();
            entity.UnitPrice.Should().Be(20m);
            entity.Status.Should().Be(DataStatus.Updated);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((MenuProduct?)null);
            var result = await _manager.UpdateAsync(new MenuProductDTO { Id = 99 }, new MenuProductDTO { Id = 99 });
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.NotFound);
        }
    }

    public class OrderDetailManagerTests
    {
        private readonly Mock<IOrderDetailRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<OrderDetailDTO>> _validatorMock = new();
        private readonly OrderDetailManager _manager;

        public OrderDetailManagerTests()
        {
            _manager = new OrderDetailManager(_repoMock.Object, _uowMock.Object, _mapperMock.Object, _validatorMock.Object, Mock.Of<ILogger<OrderDetailManager>>());
        }

        [Fact]
        public async Task UpdateDetailStateAsync_CallsRepoAndCommit()
        {
            _repoMock.Setup(r => r.UpdateDetailStateAsync(1, OrderDetailStatus.SendToTheTable)).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.UpdateDetailStateAsync(1, OrderDetailStatus.SendToTheTable);

            _repoMock.Verify(r => r.UpdateDetailStateAsync(1, OrderDetailStatus.SendToTheTable), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(default), Times.Once);
        }
    }

    public class TableManagerTests
    {
        private readonly Mock<ITableRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<TableDTO>> _validatorMock = new();
        private readonly TableManager _manager;

        public TableManagerTests()
        {
            _manager = new TableManager(_repoMock.Object, _uowMock.Object, _mapperMock.Object, _validatorMock.Object, Mock.Of<ILogger<TableManager>>());
        }

        [Fact]
        public async Task GetTablesByUserIdAsync_ReturnsList()
        {
            var tables = new List<Table> { new() { Id = 1, TableNumber = "T1" } };
            var dtos = new List<TableDTO> { new() { Id = 1, TableNumber = "T1" } };
            _repoMock.Setup(r => r.GetTablesByUserIdAsync("1")).ReturnsAsync(tables);
            _mapperMock.Setup(m => m.Map<List<TableDTO>>(tables)).Returns(dtos);

            var result = await _manager.GetTablesByUserIdAsync("1");
            result.Should().HaveCount(1);
        }
    }
}
