using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.InnerInfrastructure.ManagerConcretes;
using Xunit;

namespace Project.UnitTests
{
    public class ProductManagerTests
    {
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<ProductDTO>> _validatorMock;
        private readonly Mock<IStockTransActionManager> _stockMock;
        private readonly ProductManager _manager;

        public ProductManagerTests()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<ProductDTO>>();
            _stockMock = new Mock<IStockTransActionManager>();

            _manager = new ProductManager(
                _productRepoMock.Object,
                _uowMock.Object,
                _mapperMock.Object,
                _validatorMock.Object,
                _stockMock.Object,
                Mock.Of<ILogger<ProductManager>>());
        }

        #region IncreaseStockAsync

        [Fact]
        public async Task IncreaseStockAsync_WhenProductExists_IncreasesQuantity()
        {
            Product product = new Product { Id = 1, ProductName = "Un", Quantity = 10m };

            _productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _productRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.IncreaseStockAsync(1, 5m);

            product.Quantity.Should().Be(15m);
            _productRepoMock.Verify(r => r.UpdateAsync(product), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(default), Times.Once);
        }

        [Fact]
        public async Task IncreaseStockAsync_WhenProductNotFound_DoesNothing()
        {
            _productRepoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Product)null!);

            await _manager.IncreaseStockAsync(999, 5m);

            _productRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(default), Times.Never);
        }

        #endregion

        #region ReduceStockAfterSaleAsync

        [Fact]
        public async Task ReduceStockAfterSaleAsync_ReadyMadeProduct_DeductsDirectly()
        {
            Product product = new Product
            {
                Id = 1,
                ProductName = "Kola",
                Quantity = 20m,
                UnitPrice = 5m,
                IsReadyMade = true,
                Recipe = null
            };

            _productRepoMock.Setup(r => r.GetProductWithRecipeAsync(1)).ReturnsAsync(product);
            _productRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);
            _stockMock.Setup(s => s.CreateAsync(It.IsAny<StockTransActionDTO>()))
                .ReturnsAsync(Application.Results.Result.Succeed());

            await _manager.ReduceStockAfterSaleAsync(1, 3m, 100);

            product.Quantity.Should().Be(17m);
            _productRepoMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task ReduceStockAfterSaleAsync_WithRecipe_DeductsIngredients()
        {
            Product ingredient = new Product
            {
                Id = 10,
                ProductName = "Un",
                Quantity = 100m,
                UnitPrice = 2m
            };

            Product product = new Product
            {
                Id = 1,
                ProductName = "Ekmek",
                Quantity = 50m,
                UnitPrice = 5m,
                Recipe = new Recipe
                {
                    Id = 1,
                    Name = "Ekmek Tarifi",
                    Description = "Ekmek tarifi",
                    RecipeItems = new List<RecipeItem>
                    {
                        new RecipeItem { ProductId = 10, Quantity = 2m, Product = ingredient }
                    }
                }
            };

            _productRepoMock.Setup(r => r.GetProductWithRecipeAsync(1)).ReturnsAsync(product);
            _productRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);
            _stockMock.Setup(s => s.CreateAsync(It.IsAny<StockTransActionDTO>()))
                .ReturnsAsync(Application.Results.Result.Succeed());

            await _manager.ReduceStockAfterSaleAsync(1, 3m, 100);

            // 2 * 3 = 6 düþülmeli
            ingredient.Quantity.Should().Be(94m);
            _productRepoMock.Verify(r => r.UpdateAsync(ingredient), Times.Once);
        }

        [Fact]
        public async Task ReduceStockAfterSaleAsync_WhenProductNotFound_ThrowsException()
        {
            _productRepoMock.Setup(r => r.GetProductWithRecipeAsync(999)).ReturnsAsync((Product)null!);

            Func<Task> act = async () => await _manager.ReduceStockAfterSaleAsync(999, 1m, 1);

            await act.Should().ThrowAsync<Exception>().WithMessage("Ürün bulunamadý!");
        }

        #endregion

        #region GetProductsByCategoryIdAsync

        [Fact]
        public async Task GetProductsByCategoryIdAsync_ReturnsMappedProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, ProductName = "A", CategoryId = 5 }
            };

            List<ProductDTO> dtos = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, ProductName = "A", CategoryId = 5 }
            };

            _productRepoMock.Setup(r => r.GetByCategoryIdAsync(5)).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<List<ProductDTO>>(products)).Returns(dtos);

            List<ProductDTO> result = await _manager.GetProductsByCategoryIdAsync(5);

            result.Should().HaveCount(1);
            result[0].ProductName.Should().Be("A");
        }

        #endregion

        #region GetSellableProductsAsync

        [Fact]
        public async Task GetSellableProductsAsync_ReturnsMappedProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Kola", IsSellable = true }
            };

            List<ProductDTO> dtos = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, ProductName = "Kola", IsSellable = true }
            };

            _productRepoMock.Setup(r => r.GetSellableProductsAsync()).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<List<ProductDTO>>(products)).Returns(dtos);

            List<ProductDTO> result = await _manager.GetSellableProductsAsync();

            result.Should().HaveCount(1);
            result[0].IsSellable.Should().BeTrue();
        }

        #endregion

        #region GetWithCategory

        [Fact]
        public async Task GetWithCategory_ReturnsMappedProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, ProductName = "A", CategoryId = 1 }
            };

            List<ProductDTO> dtos = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, ProductName = "A", CategoryName = "Ýçecek" }
            };

            _productRepoMock.Setup(r => r.GetWithCategory()).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<List<ProductDTO>>(products)).Returns(dtos);

            List<ProductDTO> result = await _manager.GetWithCategory();

            result.Should().HaveCount(1);
            result[0].CategoryName.Should().Be("Ýçecek");
        }

        #endregion
    }
}
