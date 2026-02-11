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
    public class BaseRepositoryIntegrationTests
    {
        private static (SqliteConnection conn, DbContextOptions<MyContext> opts) CreateDb()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var opts = new DbContextOptionsBuilder<MyContext>().UseSqlite(conn).Options;
            using var ctx = new MyContext(opts);
            ctx.Database.EnsureCreated();
            return (conn, opts);
        }

        [Fact]
        public async Task CreateAsync_And_GetByIdAsync()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int id;
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var uow = new UnitOfWork(ctx);
                    var entity = new Category { CategoryName = "NewCat", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    await repo.CreateAsync(entity);
                    await uow.CommitAsync();
                    id = entity.Id;
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var result = await repo.GetByIdAsync(id);
                    result.Should().NotBeNull();
                    result.CategoryName.Should().Be("NewCat");
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAll()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    ctx.Categories.Add(new Category { CategoryName = "Extra1", Description = "D1", Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    ctx.Categories.Add(new Category { CategoryName = "Extra2", Description = "D2", Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var all = await repo.GetAllAsync();
                    all.Count.Should().BeGreaterThanOrEqualTo(2);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int id;
                using (var ctx = new MyContext(opts))
                {
                    var entity = new Category { CategoryName = "Old", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    ctx.Categories.Add(entity);
                    await ctx.SaveChangesAsync();
                    id = entity.Id;
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var uow = new UnitOfWork(ctx);
                    var entity = await repo.GetByIdAsync(id);
                    entity.CategoryName = "New";
                    entity.Status = DataStatus.Updated;
                    await repo.UpdateAsync(entity);
                    await uow.CommitAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var entity = await repo.GetByIdAsync(id);
                    entity.CategoryName.Should().Be("New");
                    entity.Status.Should().Be(DataStatus.Updated);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task HardDeleteAsync_RemovesEntity()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int id;
                using (var ctx = new MyContext(opts))
                {
                    var entity = new Category { CategoryName = "ToDel", Description = "D", Status = DataStatus.Deleted, InsertedDate = DateTime.Now };
                    ctx.Categories.Add(entity);
                    await ctx.SaveChangesAsync();
                    id = entity.Id;
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var uow = new UnitOfWork(ctx);
                    var entity = await repo.GetByIdAsync(id);
                    await repo.HardDeleteAsync(entity);
                    await uow.CommitAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var entity = await repo.GetByIdAsync(id);
                    entity.Should().BeNull();
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task WhereAsync_FiltersCorrectly()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    ctx.Categories.Add(new Category { CategoryName = "ActiveCat", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var result = await repo.WhereAsync(x => x.CategoryName == "ActiveCat");
                    result.Should().HaveCount(1);
                    result[0].CategoryName.Should().Be("ActiveCat");
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsCorrectPage()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    for (int i = 0; i < 10; i++)
                        ctx.Categories.Add(new Category { CategoryName = $"Paged{i}", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var (items, totalCount) = await repo.GetPagedAsync(1, 5);
                    items.Should().HaveCount(5);
                    totalCount.Should().BeGreaterThanOrEqualTo(10);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetPagedAsync_WithFilter()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    for (int i = 0; i < 5; i++)
                        ctx.Categories.Add(new Category { CategoryName = $"FilterCat{i}", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var (items, totalCount) = await repo.GetPagedAsync(1, 100, x => x.CategoryName.StartsWith("FilterCat"));
                    totalCount.Should().Be(5);
                    items.Should().HaveCount(5);
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetQuery_ReturnsQueryable()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var query = repo.GetQuery();
                    var list = await query.ToListAsync();
                    list.Should().NotBeNull();
                }
            }
            finally { conn.Dispose(); }
        }
    }

    public class CategoryRepositoryIntegrationTests
    {
        private static (SqliteConnection conn, DbContextOptions<MyContext> opts) CreateDb()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var opts = new DbContextOptionsBuilder<MyContext>().UseSqlite(conn).Options;
            using var ctx = new MyContext(opts);
            ctx.Database.EnsureCreated();
            return (conn, opts);
        }

        [Fact]
        public async Task GetRootsAsync_ReturnsNonDeletedRoots()
        {
            var (conn, opts) = CreateDb();
            try
            {
                using (var ctx = new MyContext(opts))
                {
                    ctx.Categories.Add(new Category { CategoryName = "MyRoot", Description = "D", ParentCategoryId = null, Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var roots = await repo.GetRootsAsync();
                    roots.Should().Contain(x => x.CategoryName == "MyRoot");
                }
            }
            finally { conn.Dispose(); }
        }

        [Fact]
        public async Task GetByParentIdAsync_ReturnsSubCategories()
        {
            var (conn, opts) = CreateDb();
            try
            {
                int parentId;
                using (var ctx = new MyContext(opts))
                {
                    var parent = new Category { CategoryName = "Parent", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now };
                    ctx.Categories.Add(parent);
                    await ctx.SaveChangesAsync();
                    parentId = parent.Id;
                    ctx.Categories.Add(new Category { CategoryName = "Child1", Description = "D", ParentCategoryId = parentId, Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    ctx.Categories.Add(new Category { CategoryName = "ChildDel", Description = "D", ParentCategoryId = parentId, Status = DataStatus.Deleted, InsertedDate = DateTime.Now });
                    await ctx.SaveChangesAsync();
                }
                using (var ctx = new MyContext(opts))
                {
                    var repo = new CategoryRepository(ctx);
                    var subs = await repo.GetByParentIdAsync(parentId);
                    subs.Should().HaveCount(1);
                    subs[0].CategoryName.Should().Be("Child1");
                }
            }
            finally { conn.Dispose(); }
        }
    }

    public class UnitOfWorkIntegrationTests
    {
        [Fact]
        public async Task CommitAsync_SavesChanges()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var opts = new DbContextOptionsBuilder<MyContext>().UseSqlite(conn).Options;
            using var setupCtx = new MyContext(opts);
            setupCtx.Database.EnsureCreated();

            try
            {
                using (var ctx = new MyContext(opts))
                {
                    ctx.Categories.Add(new Category { CategoryName = "UoW", Description = "D", Status = DataStatus.Inserted, InsertedDate = DateTime.Now });
                    var uow = new UnitOfWork(ctx);
                    var result = await uow.CommitAsync();
                    result.Should().BeGreaterThan(0);
                }
                using (var ctx = new MyContext(opts))
                {
                    var exists = await ctx.Categories.AnyAsync(x => x.CategoryName == "UoW");
                    exists.Should().BeTrue();
                }
            }
            finally { conn.Dispose(); }
        }
    }
}
