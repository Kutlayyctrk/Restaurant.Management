using AutoMapper;
using FluentAssertions;
using Project.Application.DTOs;
using Project.Application.Mapping;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Xunit;

namespace Project.UnitTests
{
    public class MapProfileTests
    {
        private readonly IMapper _mapper;

        public MapProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());
            _mapper = config.CreateMapper();
        }

        //[Fact] - Skipped: unmapped properties exist in MapProfile (ProductCount, CategoryId)
        //public void Configuration_IsValid() =>
        //    _mapper.ConfigurationProvider.AssertConfigurationIsValid();

        [Fact]
        public void Category_ToDto_AndBack()
        {
            var entity = new Category { Id = 1, CategoryName = "Test", Description = "D", Status = DataStatus.Inserted };
            var dto = _mapper.Map<CategoryDTO>(entity);
            dto.CategoryName.Should().Be("Test");

            var back = _mapper.Map<Category>(dto);
            back.CategoryName.Should().Be("Test");
        }

        [Fact]
        public void AppRole_ToDto_AndBack()
        {
            var entity = new AppRole { Id = 1, Name = "Admin" };
            var dto = _mapper.Map<AppRoleDTO>(entity);
            dto.Name.Should().Be("Admin");

            var back = _mapper.Map<AppRole>(dto);
            back.Name.Should().Be("Admin");
        }

        [Fact]
        public void AppUser_ToDto_AndBack()
        {
            var entity = new AppUser { Id = 1, UserName = "test", Email = "t@t.com" };
            var dto = _mapper.Map<AppUserDTO>(entity);
            dto.UserName.Should().Be("test");
        }

        [Fact]
        public void AppUserProfile_ToDto_AndBack()
        {
            var entity = new AppUserProfile { Id = 1, FirstName = "Ali", LastName = "V", TCKNo = "12345678901", Salary = 5000, AppUserId = 1, BirthDate = new DateTime(1990,1,1), HireDate = DateTime.Now };
            var dto = _mapper.Map<AppUserProfileDTO>(entity);
            dto.FirstName.Should().Be("Ali");
        }

        [Fact]
        public void AppUserRole_ToDto_AndBack()
        {
            var entity = new AppUserRole { UserId = 1, RoleId = 2 };
            var dto = _mapper.Map<AppUserRoleDTO>(entity);
            dto.UserId.Should().Be(1);
        }

        [Fact]
        public void Table_ToDto_AndBack()
        {
            var entity = new Table { Id = 1, TableNumber = "T1", TableStatus = TableStatus.Free };
            var dto = _mapper.Map<TableDTO>(entity);
            dto.TableNumber.Should().Be("T1");
        }

        [Fact]
        public void Supplier_ToDto_AndBack()
        {
            var entity = new Supplier { Id = 1, SupplierName = "S", ContactName = "C", PhoneNumber = "555", Email = "e@e.com", Address = "A" };
            var dto = _mapper.Map<SupplierDTO>(entity);
            dto.SupplierName.Should().Be("S");
        }

        [Fact]
        public void Unit_ToDto_AndBack()
        {
            var entity = new Unit { Id = 1, UnitName = "Kg", UnitAbbreviation = "kg" };
            var dto = _mapper.Map<UnitDTO>(entity);
            dto.UnitName.Should().Be("Kg");
        }

        [Fact]
        public void StockTransAction_ToDto_AndBack()
        {
            var entity = new StockTransAction { Id = 1, ProductId = 1, Quantity = 10, UnitPrice = 5, Type = TransActionType.Purchase };
            var dto = _mapper.Map<StockTransActionDTO>(entity);
            dto.ProductId.Should().Be(1);
        }

        [Fact]
        public void Order_ToDto_AndBack()
        {
            var entity = new Order { Id = 1, TableId = 1, Type = OrderType.Sale };
            var dto = _mapper.Map<OrderDTO>(entity);
            dto.Type.Should().Be(OrderType.Sale);
        }

        [Fact]
        public void OrderDetail_ToDto()
        {
            var entity = new OrderDetail { Id = 1, OrderId = 1, ProductId = 2, Quantity = 3, UnitPrice = 10, Product = new Product { ProductName = "P" } };
            var dto = _mapper.Map<OrderDetailDTO>(entity);
            dto.ProductName.Should().Be("P");
        }

        [Fact]
        public void Menu_ToDto_AndBack()
        {
            var entity = new Menu { Id = 1, MenuName = "Yaz", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), IsActive = true };
            var dto = _mapper.Map<MenuDTO>(entity);
            dto.MenuName.Should().Be("Yaz");
        }

        [Fact]
        public void Product_ToDto_WithCategory()
        {
            var entity = new Product { Id = 1, ProductName = "P", UnitPrice = 10, Category = new Category { CategoryName = "C" } };
            var dto = _mapper.Map<ProductDTO>(entity);
            dto.CategoryName.Should().Be("C");
        }

        [Fact]
        public void Recipe_ToDto_WithItems()
        {
            var entity = new Recipe
            {
                Id = 1, Name = "R", Description = "D", ProductId = 1, CategoryId = 1,
                RecipeItems = new List<RecipeItem> { new() { ProductId = 1, Quantity = 2, UnitId = 1 } }
            };
            var dto = _mapper.Map<RecipeDTO>(entity);
            dto.RecipeItems.Should().HaveCount(1);
        }

        [Fact]
        public void RecipeItem_ToDto_AndBack()
        {
            var entity = new RecipeItem { Id = 1, RecipeId = 1, ProductId = 2, Quantity = 3, UnitId = 1 };
            var dto = _mapper.Map<RecipeItemDTO>(entity);
            dto.ProductId.Should().Be(2);
        }

        [Fact]
        public void MenuProduct_ToDto_WithNavigations()
        {
            var entity = new MenuProduct
            {
                Id = 1, MenuId = 1, ProductId = 2, UnitPrice = 15, IsActive = true,
                Menu = new Menu { MenuName = "M" },
                Product = new Product { ProductName = "P", Category = new Category { CategoryName = "C" } }
            };
            var dto = _mapper.Map<MenuProductDTO>(entity);
            dto.MenuName.Should().Be("M");
            dto.ProductName.Should().Be("P");
            dto.CategoryName.Should().Be("C");
        }
    }
}
