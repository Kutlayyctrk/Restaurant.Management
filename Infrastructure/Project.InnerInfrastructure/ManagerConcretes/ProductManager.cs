using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Abstract;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class ProductManager(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductDTO> productValidator, IStockTransActionManager stockTransActionManager, ILogger<ProductManager> logger) : BaseManager<Product, ProductDTO>(productRepository, unitOfWork, mapper, productValidator, logger), IProductManager
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IStockTransActionManager _stockTransActionManager = stockTransActionManager;

        public async Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
        {
            List<Product> products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<List<ProductDTO>> GetSellableProductsAsync()
        {
            List<Product> products = await _productRepository.GetSellableProductsAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<List<ProductDTO>> GetWithCategory()
        {
            List<Product> products = await _productRepository.GetWithCategory();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task IncreaseStockAsync(int productId, decimal quantity)
        {
            Product product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                product.Quantity += quantity;
                await _productRepository.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task ReduceStockAfterSaleAsync(int productId, decimal orderQuantity, int orderDetailId)
        {
            Product product = await _productRepository.GetProductWithRecipeAsync(productId);

            if (product == null) throw new Exception("Ürün bulunamadı!");

            if (product.Recipe != null && product.Recipe.RecipeItems != null && product.Recipe.RecipeItems.Any())
            {
                foreach (RecipeItem item in product.Recipe.RecipeItems)
                {
                    decimal totalDeduction = item.Quantity * orderQuantity;

                    Product ingredient = item.Product;
                    ingredient.Quantity -= totalDeduction;

                    StockTransActionDTO stockAction = new StockTransActionDTO
                    {
                        ProductId = ingredient.Id,
                        Quantity = totalDeduction,
                        OrderDetailId = orderDetailId,
                        Type = TransActionType.Sale,
                        Description = $"{product.ProductName} satışı nedeniyle reçeteden düşüldü.",
                        UnitPrice = ingredient.UnitPrice,
                        InsertedDate = DateTime.Now
                    };

                    await _productRepository.UpdateAsync(ingredient);
                    await _unitOfWork.CommitAsync();
                    await _stockTransActionManager.CreateAsync(stockAction);
                }
            }
            else
            {
                product.Quantity -= orderQuantity;

                StockTransActionDTO stockAction = new StockTransActionDTO
                {
                    ProductId = product.Id,
                    Quantity = orderQuantity,
                    OrderDetailId = orderDetailId,
                    Type = TransActionType.Sale,
                    Description = "Doğrudan ürün satışı (Hazır ürün)",
                    UnitPrice = product.UnitPrice,
                    InsertedDate = DateTime.Now
                };

                await _productRepository.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
                await _stockTransActionManager.CreateAsync(stockAction);
            }
        }
    }
}