using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.Persistance.ContextClasses;
using Project.Persistance.Repositories;
using Xunit;

namespace Project.IntegrationTests
{
    public class RepositoryTestBase
    {
        protected static (SqliteConnection conn, DbContextOptions<MyContext> opts) CreateDb()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var opts = new DbContextOptionsBuilder<MyContext>().UseSqlite(conn).Options;
            using var ctx = new MyContext(opts);
            ctx.Database.EnsureCreated();
            return (conn, opts);
        }

        protected static async Task<(Unit unit, Category category)> SeedUnitAndCategory(MyContext ctx)
        {
            var unit = new Unit { UnitName = "adet", UnitAbbreviation = "ad", Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
            var cat = new Category { CategoryName = "TestCat", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
            ctx.Units.Add(unit);
            ctx.Categories.Add(cat);
            await ctx.SaveChangesAsync();
            return (unit, cat);
        }

        protected static async Task<Product> SeedProduct(MyContext ctx, int unitId, int catId, string name = "TestProd")
        {
            var p = new Product { ProductName = name, UnitPrice = 10m, UnitId = unitId, CategoryId = catId, Quantity = 0, Status = DataStatus.Inserted, InsertedDate = DateTime.Now, IsSellable = true };
            ctx.Products.Add(p);
            await ctx.SaveChangesAsync();
            return p;
        }
    }

    public class ProductRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public async Task GetByCategoryIdAsync_ReturnsProducts()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int catId;
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    catId = cat.Id;
                    await SeedProduct(ctx, unit.Id, catId, "P1");
                    await SeedProduct(ctx, unit.Id, catId, "P2");
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new ProductRepository(ctx);
                    var result = await repo.GetByCategoryIdAsync(catId);
                    result.Should().HaveCountGreaterThanOrEqualTo(2);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetSellableProductsAsync_ReturnsSellable()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var p = await SeedProduct(ctx, unit.Id, cat.Id, "Sellable");
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new ProductRepository(ctx);
                    var result = await repo.GetSellableProductsAsync();
                    result.Should().Contain(x => x.ProductName == "Sellable");
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetWithCategory_IncludesCategory()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    await SeedProduct(ctx, unit.Id, cat.Id, "WithCat");
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new ProductRepository(ctx);
                    var result = await repo.GetWithCategory();
                    result.Should().Contain(x => x.ProductName == "WithCat");
                    result.First(x => x.ProductName == "WithCat").Category.Should().NotBeNull();
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetProductWithRecipeAsync_IncludesRecipe()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int prodId;
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id, "WithRecipe");
                    prodId = prod.Id;
                    var ingredient = await SeedProduct(ctx, unit.Id, cat.Id, "Ingredient");
                    ctx.Recipes.Add(new Recipe
                    {
                        Name = "R", Description = "D", ProductId = prodId, CategoryId = cat.Id, Status = DataStatus.Inserted, InsertedDate = DateTime.Now,
                        RecipeItems = new List<RecipeItem> { new() { ProductId = ingredient.Id, UnitId = unit.Id, Quantity = 1, Status = DataStatus.Inserted, InsertedDate = DateTime.Now } }
                    });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new ProductRepository(ctx);
                    var result = await repo.GetProductWithRecipeAsync(prodId);
                    result.Should().NotBeNull();
                }
            }
            finally { conn.Dispose(); }
        }
    }

    public class TableRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public async Task GetTablesByUserIdAsync_ValidId_ReturnsTables()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    ctx.Tables.Add(new Table { TableNumber = "T99", TableStatus = TableStatus.Free, WaiterId = 5, Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    ctx.Tables.Add(new Table { TableNumber = "T100", TableStatus = TableStatus.Free, WaiterId = null, Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new TableRepository(ctx);
                    var result = await repo.GetTablesByUserIdAsync("5");
                    result.Should().Contain(x => x.TableNumber == "T99");
                    result.Should().Contain(x => x.TableNumber == "T100");
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetTablesByUserIdAsync_InvalidId_ReturnsEmpty()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var repo = new TableRepository(ctx);
                    var result = await repo.GetTablesByUserIdAsync("abc");
                    result.Should().BeEmpty();
                }
            }
            finally { conn.Dispose(); }
        }
    }

    public class MenuProductRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public async Task GetWithMenuAndProduct_IncludesNavigations()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id);
                    var menu = new Menu { MenuName = "M1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), IsActive = true, Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    ctx.Menus.Add(menu);
                    await ctx.SaveChangesAsync();
                    ctx.MenuProducts.Add(new MenuProduct { MenuId = menu.Id, ProductId = prod.Id, UnitPrice = 15, IsActive = true, Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new MenuProductRepository(ctx);
                    var result = await repo.GetWithMenuAndProduct();
                    result.Should().NotBeEmpty();
                    result.First().Menu.Should().NotBeNull();
                    result.First().Product.Should().NotBeNull();
                }
            }
            finally { conn.Dispose(); }
        }
    }

    public class RecipeRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public async Task GetAllAsync_IncludesItems()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id, "RecipeProd");
                    var ingredient = await SeedProduct(ctx, unit.Id, cat.Id, "Ing");
                    ctx.Recipes.Add(new Recipe
                    {
                        Name = "R1", Description = "D", ProductId = prod.Id, CategoryId = cat.Id, Status = DataStatus.Inserted, InsertedDate = DateTime.Now,
                        RecipeItems = new List<RecipeItem> { new() { ProductId = ingredient.Id, UnitId = unit.Id, Quantity = 2, Status = DataStatus.Inserted, InsertedDate = DateTime.Now } }
                    });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new RecipeRepository(ctx);
                    var all = await repo.GetAllAsync();
                    all.Should().Contain(x => x.Name == "R1");
                    all.First(x => x.Name == "R1").RecipeItems.Should().NotBeEmpty();
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetByProductIdAsync_ReturnsRecipe()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int prodId;
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id);
                    prodId = prod.Id;
                    var ing = await SeedProduct(ctx, unit.Id, cat.Id, "Ing2");
                    ctx.Recipes.Add(new Recipe
                    {
                        Name = "R2", Description = "D", ProductId = prodId, CategoryId = cat.Id, Status = DataStatus.Inserted, InsertedDate = DateTime.Now,
                        RecipeItems = new List<RecipeItem> { new() { ProductId = ing.Id, UnitId = unit.Id, Quantity = 1, Status = DataStatus.Inserted, InsertedDate = DateTime.Now } }
                    });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new RecipeRepository(ctx);
                    var result = await repo.GetByProductIdAsync(prodId);
                    result.Should().NotBeNull();
                    result!.RecipeItems.Should().NotBeEmpty();
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetByIdWithItemsAsync_ReturnsWithItems()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int recipeId;
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id);
                    var ing = await SeedProduct(ctx, unit.Id, cat.Id, "Ing3");
                    var recipe = new Recipe
                    {
                        Name = "R3", Description = "D", ProductId = prod.Id, CategoryId = cat.Id, Status = DataStatus.Inserted, InsertedDate = DateTime.Now,
                        RecipeItems = new List<RecipeItem> { new() { ProductId = ing.Id, UnitId = unit.Id, Quantity = 3, Status = DataStatus.Inserted, InsertedDate = DateTime.Now } }
                    };
                    ctx.Recipes.Add(recipe);
                    await ctx.SaveChangesAsync();
                    recipeId = recipe.Id;
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new RecipeRepository(ctx);
                    var result = await repo.GetByIdWithItemsAsync(recipeId);
                    result.Should().NotBeNull();
                    result!.RecipeItems.Should().HaveCount(1);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task CreateAsync_AddsRecipe()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id);
                    var repo = new RecipeRepository(ctx);
                    await repo.CreateAsync(new Recipe { Name = "Created", Description = "D", ProductId = prod.Id, CategoryId = cat.Id, Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var exists = await ctx.Recipes.AnyAsync(x => x.Name == "Created");
                    exists.Should().BeTrue();
                }
            }
            finally { conn.Dispose(); }
        }
    }

    public class OrderDetailRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public async Task UpdateDetailStateAsync_UpdatesState()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int detailId;
                using (var ctx = new MyContext(opts))
                {
                    var (unit, cat) = await SeedUnitAndCategory(ctx);
                    var prod = await SeedProduct(ctx, unit.Id, cat.Id);
                    var table = new Table { TableNumber = "T1", TableStatus = TableStatus.Free, Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    ctx.Tables.Add(table);
                    await ctx.SaveChangesAsync();
                    var order = new Order { TableId = table.Id, Type = OrderType.Sale, OrderState = OrderStatus.SentToKitchen, OrderDate = DateTime.Now, TotalPrice = 0, Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    ctx.Orders.Add(order);
                    await ctx.SaveChangesAsync();
                    var detail = new OrderDetail { OrderId = order.Id, ProductId = prod.Id, Quantity = 1, UnitPrice = 10, DetailState = OrderDetailStatus.SendToKitchen, Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    ctx.OrderDetails.Add(detail);
                    await ctx.SaveChangesAsync();
                    detailId = detail.Id;
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new OrderDetailRepository(ctx);
                    var uow = new UnitOfWork(ctx);
                    await repo.UpdateDetailStateAsync(detailId, OrderDetailStatus.SendToTheTable);
                    await uow.CommitAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var detail = await ctx.OrderDetails.FindAsync(detailId);
                    detail!.DetailState.Should().Be(OrderDetailStatus.SendToTheTable);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task UpdateDetailStateAsync_NonExistent_DoesNothing()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var repo = new OrderDetailRepository(ctx);
                    await repo.UpdateDetailStateAsync(9999, OrderDetailStatus.Cancelled);
                    // should not throw
                }
            }
            finally { conn.Dispose(); }
        }
    }
}
