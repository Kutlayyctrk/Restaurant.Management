using FluentAssertions;
using Project.Application.DTOs;
using Project.Domain.Entities.Abstract;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Xunit;

namespace Project.UnitTests
{
    public class EntityPropertyTests
    {
        [Fact]
        public void AppRole_Properties()
        {
            var role = new AppRole
            {
                Id = 1, Name = "Admin",
                InsertedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                DeletionDate = DateTime.Now,
                Status = DataStatus.Inserted
            };
            role.Id.Should().Be(1);
            role.Name.Should().Be("Admin");
            role.Status.Should().Be(DataStatus.Inserted);
            role.UpdatedDate.Should().NotBeNull();
            role.DeletionDate.Should().NotBeNull();
            role.UserRoles.Should().BeNull();
        }

        [Fact]
        public void AppUser_Properties()
        {
            var user = new AppUser
            {
                Id = 1, UserName = "test", Email = "t@t.com",
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            user.Id.Should().Be(1);
            user.UserName.Should().Be("test");
            user.Email.Should().Be("t@t.com");
            user.AppUserProfile.Should().BeNull();
            user.UserRoles.Should().BeNull();
            user.Orders.Should().BeNull();
            user.Tables.Should().BeNull();
        }

        [Fact]
        public void AppUserProfile_Properties()
        {
            var p = new AppUserProfile
            {
                Id = 1, FirstName = "Ali", LastName = "Veli",
                TCKNo = "12345678901", Salary = 5000m,
                BirthDate = new DateTime(1990,1,1),
                HireDate = DateTime.Now,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted,
                AppUserId = 10
            };
            p.FirstName.Should().Be("Ali");
            p.LastName.Should().Be("Veli");
            p.TCKNo.Should().Be("12345678901");
            p.Salary.Should().Be(5000m);
            p.AppUserId.Should().Be(10);
            p.AppUser.Should().BeNull();
        }

        [Fact]
        public void AppUserRole_Properties()
        {
            var ur = new AppUserRole
            {
                UserId = 1, RoleId = 2,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            ur.UserId.Should().Be(1);
            ur.RoleId.Should().Be(2);
            ur.User.Should().BeNull();
            ur.Role.Should().BeNull();
        }

        [Fact]
        public void Category_Properties()
        {
            var c = new Category
            {
                Id = 1, CategoryName = "Test", Description = "Desc",
                ParentCategoryId = null,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            c.CategoryName.Should().Be("Test");
            c.Description.Should().Be("Desc");
            c.ParentCategoryId.Should().BeNull();
            c.ParentCategory.Should().BeNull();
            c.SubCategories.Should().BeNull();
            c.Products.Should().BeNull();
            c.Recipes.Should().BeNull();
        }

        [Fact]
        public void Category_WithParent()
        {
            var c = new Category { Id = 2, CategoryName = "Sub", ParentCategoryId = 1 };
            c.ParentCategoryId.Should().Be(1);
        }

        [Fact]
        public void Table_Properties()
        {
            var t = new Table
            {
                Id = 1, TableNumber = "T1", TableStatus = TableStatus.Free,
                WaiterId = 5,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            t.TableNumber.Should().Be("T1");
            t.TableStatus.Should().Be(TableStatus.Free);
            t.WaiterId.Should().Be(5);
            t.Waiter.Should().BeNull();
            t.Orders.Should().BeNull();
        }

        [Fact]
        public void Table_NullWaiter()
        {
            var t = new Table { TableNumber = "T2", WaiterId = null };
            t.WaiterId.Should().BeNull();
        }

        [Fact]
        public void Unit_Properties()
        {
            var u = new Unit
            {
                Id = 1, UnitName = "Kilogram", UnitAbbreviation = "kg",
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            u.UnitName.Should().Be("Kilogram");
            u.UnitAbbreviation.Should().Be("kg");
            u.Products.Should().BeNull();
            u.RecipeItems.Should().BeNull();
        }

        [Fact]
        public void Menu_Properties()
        {
            var m = new Menu
            {
                Id = 1, MenuName = "Yaz", IsActive = true,
                StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30),
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            m.MenuName.Should().Be("Yaz");
            m.IsActive.Should().BeTrue();
            m.MenuProducts.Should().BeNull();
        }

        [Fact]
        public void MenuProduct_Properties()
        {
            var mp = new MenuProduct
            {
                Id = 1, MenuId = 1, ProductId = 2, UnitPrice = 15m, IsActive = true,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            mp.MenuId.Should().Be(1);
            mp.ProductId.Should().Be(2);
            mp.UnitPrice.Should().Be(15m);
            mp.IsActive.Should().BeTrue();
            mp.Menu.Should().BeNull();
            mp.Product.Should().BeNull();
        }

        [Fact]
        public void OrderDetail_Properties()
        {
            var od = new OrderDetail
            {
                Id = 1, OrderId = 1, ProductId = 2, Quantity = 3, UnitPrice = 10m,
                DetailState = OrderDetailStatus.SendToKitchen,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            od.OrderId.Should().Be(1);
            od.ProductId.Should().Be(2);
            od.Quantity.Should().Be(3);
            od.UnitPrice.Should().Be(10m);
            od.DetailState.Should().Be(OrderDetailStatus.SendToKitchen);
            od.Order.Should().BeNull();
            od.Product.Should().BeNull();
        }

        [Fact]
        public void Supplier_Properties()
        {
            var s = new Supplier
            {
                Id = 1, SupplierName = "Test", ContactName = "Ali",
                PhoneNumber = "555", Email = "t@t.com", Address = "Addr",
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            s.SupplierName.Should().Be("Test");
            s.ContactName.Should().Be("Ali");
            s.PhoneNumber.Should().Be("555");
            s.Email.Should().Be("t@t.com");
            s.Address.Should().Be("Addr");
            s.StockTransActions.Should().BeNull();
        }

        [Fact]
        public void Recipe_Properties()
        {
            var r = new Recipe
            {
                Id = 1, Name = "Tarif", Description = "Desc",
                ProductId = 1, CategoryId = 2,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            r.Name.Should().Be("Tarif");
            r.Description.Should().Be("Desc");
            r.ProductId.Should().Be(1);
            r.CategoryId.Should().Be(2);
            r.Product.Should().BeNull();
            r.Category.Should().BeNull();
            r.RecipeItems.Should().BeNull();
        }

        [Fact]
        public void RecipeItem_Properties()
        {
            var ri = new RecipeItem
            {
                Id = 1, RecipeId = 1, ProductId = 2, Quantity = 3m, UnitId = 1,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            ri.RecipeId.Should().Be(1);
            ri.ProductId.Should().Be(2);
            ri.Quantity.Should().Be(3m);
            ri.UnitId.Should().Be(1);
            ri.Recipe.Should().BeNull();
            ri.Product.Should().BeNull();
            ri.Unit.Should().BeNull();
        }

        [Fact]
        public void StockTransAction_Properties()
        {
            var st = new StockTransAction
            {
                Id = 1, ProductId = 1, Quantity = 10, UnitPrice = 5m,
                Type = TransActionType.Purchase, Description = "Test",
                SupplierId = 1,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            st.ProductId.Should().Be(1);
            st.Quantity.Should().Be(10);
            st.UnitPrice.Should().Be(5m);
            st.Type.Should().Be(TransActionType.Purchase);
            st.Description.Should().Be("Test");
            st.SupplierId.Should().Be(1);
            st.Product.Should().BeNull();
            st.Supplier.Should().BeNull();
        }

        [Fact]
        public void Product_Properties()
        {
            var p = new Product
            {
                Id = 1, ProductName = "Domates", UnitPrice = 10m,
                UnitId = 1, CategoryId = 1,
                IsExtra = false, IsSellable = true, IsReadyMade = false, CanBeProduced = true,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            p.ProductName.Should().Be("Domates");
            p.UnitPrice.Should().Be(10m);
            p.IsExtra.Should().BeFalse();
            p.IsSellable.Should().BeTrue();
            p.IsReadyMade.Should().BeFalse();
            p.CanBeProduced.Should().BeTrue();
            p.Unit.Should().BeNull();
            p.Category.Should().BeNull();
        }

        [Fact]
        public void Order_Properties()
        {
            var o = new Order
            {
                Id = 1, TableId = 1, Type = OrderType.Sale,
                OrderState = OrderStatus.SentToKitchen,
                InsertedDate = DateTime.Now, Status = DataStatus.Inserted
            };
            o.TableId.Should().Be(1);
            o.Type.Should().Be(OrderType.Sale);
            o.OrderState.Should().Be(OrderStatus.SentToKitchen);
            o.Table.Should().BeNull();
            o.OrderDetails.Should().BeNull();
        }
    }

    public class DtoPropertyTests
    {
        [Fact]
        public void AppRoleDTO_Properties()
        {
            var dto = new AppRoleDTO { Id = 1, Name = "Admin", Status = DataStatus.Inserted };
            dto.Id.Should().Be(1);
            dto.Name.Should().Be("Admin");
        }

        [Fact]
        public void AppUserDTO_Properties()
        {
            var dto = new AppUserDTO
            {
                Id = 1, UserName = "test", Email = "t@t.com",
                EmailConfirmed = true, Password = "pass",
                RememberMe = true, RoleIds = new List<int> { 1, 2 }
            };
            dto.UserName.Should().Be("test");
            dto.EmailConfirmed.Should().BeTrue();
            dto.RememberMe.Should().BeTrue();
            dto.RoleIds.Should().HaveCount(2);
        }

        [Fact]
        public void AppUserProfileDTO_Properties()
        {
            var dto = new AppUserProfileDTO
            {
                FirstName = "Ali", LastName = "Veli", TCKNo = "12345678901",
                Salary = 5000m, HireDate = DateTime.Now, BirthDate = new DateTime(1990,1,1),
                AppUserId = 1
            };
            dto.FirstName.Should().Be("Ali");
            dto.AppUserId.Should().Be(1);
        }

        [Fact]
        public void AppUserRoleDTO_Properties()
        {
            var dto = new AppUserRoleDTO { UserId = 1, RoleId = 2, UserName = "u", RoleName = "r" };
            dto.UserName.Should().Be("u");
            dto.RoleName.Should().Be("r");
        }

        [Fact]
        public void MenuDTO_Properties()
        {
            var dto = new MenuDTO
            {
                MenuName = "Test", StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1), IsActive = true, ProductCount = 5
            };
            dto.MenuName.Should().Be("Test");
            dto.ProductCount.Should().Be(5);
        }

        [Fact]
        public void MenuProductDTO_Properties()
        {
            var dto = new MenuProductDTO
            {
                MenuId = 1, MenuName = "M", ProductId = 2, ProductName = "P",
                UnitPrice = 10m, IsActive = true, CategoryId = 3, CategoryName = "C"
            };
            dto.MenuName.Should().Be("M");
            dto.CategoryName.Should().Be("C");
        }

        [Fact]
        public void RecipeDTO_Properties()
        {
            var dto = new RecipeDTO
            {
                Name = "R", Description = "D", ProductId = 1, CategoryId = 2,
                ProductName = "P", CategoryName = "C",
                RecipeItems = new List<RecipeItemDTO> { new() { ProductId = 1 } }
            };
            dto.ProductName.Should().Be("P");
            dto.RecipeItems.Should().HaveCount(1);
        }

        [Fact]
        public void RecipeItemDTO_Properties()
        {
            var dto = new RecipeItemDTO
            {
                RecipeId = 1, ProductId = 2, ProductName = "P",
                Quantity = 5m, UnitId = 1, UnitName = "Kg"
            };
            dto.RecipeId.Should().Be(1);
            dto.ProductName.Should().Be("P");
            dto.UnitName.Should().Be("Kg");
        }

        [Fact]
        public void SupplierDTO_Properties()
        {
            var dto = new SupplierDTO
            {
                SupplierName = "S", ContactName = "C", PhoneNumber = "555",
                Email = "e@e.com", Address = "A", TransactionCount = 10
            };
            dto.TransactionCount.Should().Be(10);
        }

        [Fact]
        public void UnitDTO_Properties()
        {
            var dto = new UnitDTO { UnitName = "Kg", UnitAbbreviation = "kg" };
            dto.UnitName.Should().Be("Kg");
            dto.UnitAbbreviation.Should().Be("kg");
        }

        [Fact]
        public void BaseDto_Properties()
        {
            var dto = new CategoryDTO
            {
                Id = 1,
                InsertedDate = new DateTime(2024,1,1),
                UpdatedDate = new DateTime(2024,2,1),
                DeletionDate = new DateTime(2024,3,1),
                Status = DataStatus.Updated,
                CategoryName = "Test"
            };
            dto.InsertedDate.Should().Be(new DateTime(2024,1,1));
            dto.UpdatedDate.Should().Be(new DateTime(2024,2,1));
            dto.DeletionDate.Should().Be(new DateTime(2024,3,1));
            dto.Status.Should().Be(DataStatus.Updated);
        }
    }
}
