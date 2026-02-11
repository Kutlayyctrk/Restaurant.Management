using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Project.Application.DTOs;
using Project.Domain.Enums;
using Project.Validator.Validations;
using Xunit;

namespace Project.UnitTests
{
    public class ProductValidatorTests
    {
        private readonly ProductValidator _validator = new ProductValidator();

        [Fact]
        public void Valid_Product_PassesValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Domates",
                UnitPrice = 10m,
                UnitId = 1,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void EmptyProductName_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "",
                UnitId = 1,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "ProductName");
        }

        [Fact]
        public void ProductName_TooShort_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "AB",
                UnitId = 1,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ProductName_TooLong_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = new string('A', 151),
                UnitId = 1,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ProductName_WithLeadingSpace_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = " Domates",
                UnitId = 1,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void NegativeUnitPrice_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Domates",
                UnitPrice = -1m,
                UnitId = 1,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroUnitId_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Domates",
                UnitId = 0,
                CategoryId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroCategoryId_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Domates",
                UnitId = 1,
                CategoryId = 0
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Extra_NotSellable_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Ekstra Sos",
                UnitId = 1,
                CategoryId = 1,
                IsExtra = true,
                IsSellable = false
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Extra_Sellable_PassesValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Ekstra Sos",
                UnitId = 1,
                CategoryId = 1,
                IsExtra = true,
                IsSellable = true
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ReadyMade_CanBeProduced_FailsValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Hazýr Ürün",
                UnitId = 1,
                CategoryId = 1,
                IsReadyMade = true,
                CanBeProduced = true
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ReadyMade_CannotBeProduced_PassesValidation()
        {
            ProductDTO dto = new ProductDTO
            {
                ProductName = "Hazýr Ürün",
                UnitId = 1,
                CategoryId = 1,
                IsReadyMade = true,
                CanBeProduced = false
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }
    }

    public class CategoryValidatorTests
    {
        private readonly CategoryValidator _validator = new CategoryValidator();

        [Fact]
        public void Valid_Category_PassesValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "Ýçecekler",
                Description = "Soðuk ve sýcak içecekler"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void EmptyCategoryName_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "",
                Description = "Test"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CategoryName_TooShort_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "AB",
                Description = "Test"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CategoryName_TooLong_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = new string('A', 51),
                Description = "Test"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CategoryName_WithTrailingSpace_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "Ýçecek ",
                Description = "Test"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Description_TooLong_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "Ýçecekler",
                Description = new string('X', 251)
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ParentCategoryId_Zero_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "Alt Kategori",
                Description = "Test",
                ParentCategoryId = 0
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ParentCategoryId_SameAsId_FailsValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                Id = 5,
                CategoryName = "Kategori",
                Description = "Test",
                ParentCategoryId = 5
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ParentCategoryId_ValidPositive_PassesValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                Id = 5,
                CategoryName = "Alt Kategori",
                Description = "Test",
                ParentCategoryId = 3
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ParentCategoryId_Null_PassesValidation()
        {
            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = "Kök Kategori",
                Description = "Test",
                ParentCategoryId = null
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }
    }

    public class TableValidatorTests
    {
        private readonly TableValidator _validator = new TableValidator();

        [Fact]
        public void Valid_Table_PassesValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = "T1"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void EmptyTableNumber_FailsValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = ""
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void TableNumber_TooLong_FailsValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = new string('A', 21)
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void TableNumber_WithLeadingSpace_FailsValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = " T1"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void WaiterId_Zero_FailsValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = "T1",
                WaiterId = 0
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void WaiterId_Positive_PassesValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = "T1",
                WaiterId = 5
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void WaiterId_Null_PassesValidation()
        {
            TableDTO dto = new TableDTO
            {
                TableNumber = "T1",
                WaiterId = null
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }
    }

    public class OrderDetailValidatorTests
    {
        private readonly OrderDetailValidator _validator = new OrderDetailValidator();

        [Fact]
        public void Valid_OrderDetail_PassesValidation()
        {
            OrderDetailDTO dto = new OrderDetailDTO
            {
                ProductId = 1,
                Quantity = 5,
                UnitPrice = 10m,
                DetailState = OrderDetailStatus.SendToKitchen
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ZeroProductId_FailsValidation()
        {
            OrderDetailDTO dto = new OrderDetailDTO
            {
                ProductId = 0,
                Quantity = 5,
                UnitPrice = 10m
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroQuantity_FailsValidation()
        {
            OrderDetailDTO dto = new OrderDetailDTO
            {
                ProductId = 1,
                Quantity = 0,
                UnitPrice = 10m
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void NegativeUnitPrice_FailsValidation()
        {
            OrderDetailDTO dto = new OrderDetailDTO
            {
                ProductId = 1,
                Quantity = 1,
                UnitPrice = -5m
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }
    }

    public class OrderValidatorTests
    {
        private readonly OrderValidator _validator;

        public OrderValidatorTests()
        {
            _validator = new OrderValidator(new OrderDetailValidator());
        }

        [Fact]
        public void Valid_PurchaseOrder_PassesValidation()
        {
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                SupplierId = 1,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 5, UnitPrice = 10m }
                }
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void NullOrderDetails_FailsValidation()
        {
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = null!
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyOrderDetails_FailsValidation()
        {
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = new List<OrderDetailDTO>()
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Sale_WithZeroTableId_FailsValidation()
        {
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Sale,
                TableId = 0,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 1, UnitPrice = 5m }
                }
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Sale_WithTableId_PassesValidation()
        {
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Sale,
                TableId = 1,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 1, Quantity = 1, UnitPrice = 5m }
                }
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void InvalidOrderDetail_InCollection_FailsValidation()
        {
            OrderDTO dto = new OrderDTO
            {
                Type = OrderType.Purchase,
                OrderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 0, Quantity = 0, UnitPrice = -1m }
                }
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }
    }

    public class StockTransActionValidatorTests
    {
        private readonly StockTransActionValidator _validator = new StockTransActionValidator();

        [Fact]
        public void Valid_PurchaseAction_PassesValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 10,
                UnitPrice = 5m,
                Type = TransActionType.Purchase
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ZeroProductId_FailsValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 0,
                Quantity = 10,
                Type = TransActionType.Purchase
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroQuantity_FailsValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 0,
                Type = TransActionType.Purchase
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void NegativeUnitPrice_FailsValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 10,
                UnitPrice = -1m,
                Type = TransActionType.Purchase
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Return_WithoutSupplierId_FailsValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 5,
                Type = TransActionType.Return,
                SupplierId = null
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Return_WithSupplierId_PassesValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 5,
                UnitPrice = 10m,
                Type = TransActionType.Return,
                SupplierId = 1
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Waste_WithoutDescription_FailsValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 2,
                Type = TransActionType.Waste,
                Description = null
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Waste_WithDescription_PassesValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 5m,
                Type = TransActionType.Waste,
                Description = "Bozulmuþ ürün"
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Description_TooLong_FailsValidation()
        {
            StockTransActionDTO dto = new StockTransActionDTO
            {
                ProductId = 1,
                Quantity = 2,
                Type = TransActionType.Waste,
                Description = new string('X', 501)
            };

            ValidationResult result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
        }
    }
}
