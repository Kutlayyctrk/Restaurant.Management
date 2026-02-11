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
using System.Linq.Expressions;
using Xunit;

namespace Project.UnitTests
{
    public class BaseManagerTests
    {
        private readonly Mock<IRepository<Category>> _repoMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CategoryDTO>> _validatorMock;
        private readonly Mock<ICategoryRepository> _categoryRepoMock;
        private readonly CategoryManager _manager;

        public BaseManagerTests()
        {
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _repoMock = _categoryRepoMock.As<IRepository<Category>>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<CategoryDTO>>();

            _manager = new CategoryManager(
                _categoryRepoMock.Object,
                _uowMock.Object,
                _mapperMock.Object,
                _validatorMock.Object,
                Mock.Of<ILogger<CategoryManager>>());
        }

        #region CreateAsync

        [Fact]
        public async Task CreateAsync_WhenValid_ReturnsSuccess()
        {
            CategoryDTO dto = new CategoryDTO { CategoryName = "Test", Description = "Açýklama" };
            Category entity = new Category { Id = 1, CategoryName = "Test", Description = "Açýklama" };

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult());

            _mapperMock.Setup(m => m.Map<Category>(dto)).Returns(entity);

            _categoryRepoMock.Setup(r => r.CreateAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            Result result = await _manager.CreateAsync(dto);

            result.IsSuccess.Should().BeTrue();
            _categoryRepoMock.Verify(r => r.CreateAsync(It.IsAny<Category>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(default), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenValidationFails_ReturnsFailure()
        {
            CategoryDTO dto = new CategoryDTO { CategoryName = "", Description = "" };

            List<ValidationFailure> failures = new List<ValidationFailure>
            {
                new ValidationFailure("CategoryName", "Kategori adý zorunludur.")
            };

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult(failures));

            Result result = await _manager.CreateAsync(dto);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.ValidationError);
            _categoryRepoMock.Verify(r => r.CreateAsync(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_SetsInsertedDateAndStatus()
        {
            CategoryDTO dto = new CategoryDTO { CategoryName = "Test", Description = "Açýklama" };
            Category capturedEntity = null!;

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult());

            _mapperMock.Setup(m => m.Map<Category>(dto))
                .Returns(new Category { CategoryName = "Test", Description = "Açýklama" });

            _categoryRepoMock.Setup(r => r.CreateAsync(It.IsAny<Category>()))
                .Callback<Category>(e => capturedEntity = e)
                .Returns(Task.CompletedTask);

            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.CreateAsync(dto);

            capturedEntity.Should().NotBeNull();
            capturedEntity.Status.Should().Be(DataStatus.Inserted);
            capturedEntity.InsertedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
        }

        #endregion

        #region UpdateAsync

        [Fact]
        public async Task UpdateAsync_WhenValid_ReturnsSuccess()
        {
            CategoryDTO originalDto = new CategoryDTO { Id = 1 };
            CategoryDTO newDto = new CategoryDTO { Id = 1, CategoryName = "Updated", Description = "Yeni" };
            Category existingEntity = new Category { Id = 1, CategoryName = "Old", Description = "Eski" };

            _validatorMock.Setup(v => v.ValidateAsync(newDto, default))
                .ReturnsAsync(new ValidationResult());

            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingEntity);

            _mapperMock.Setup(m => m.Map(newDto, existingEntity));

            _categoryRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            Result result = await _manager.UpdateAsync(originalDto, newDto);

            result.IsSuccess.Should().BeTrue();
            _categoryRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenValidationFails_ReturnsFailure()
        {
            CategoryDTO originalDto = new CategoryDTO { Id = 1 };
            CategoryDTO newDto = new CategoryDTO { Id = 1, CategoryName = "" };

            _validatorMock.Setup(v => v.ValidateAsync(newDto, default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("CategoryName", "Zorunlu")
                }));

            Result result = await _manager.UpdateAsync(originalDto, newDto);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.ValidationError);
            _categoryRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotFound_ReturnsNotFound()
        {
            CategoryDTO originalDto = new CategoryDTO { Id = 999 };
            CategoryDTO newDto = new CategoryDTO { Id = 999, CategoryName = "Test", Description = "Test" };

            _validatorMock.Setup(v => v.ValidateAsync(newDto, default))
                .ReturnsAsync(new ValidationResult());

            _categoryRepoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null!);

            Result result = await _manager.UpdateAsync(originalDto, newDto);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.NotFound);
        }

        [Fact]
        public async Task UpdateAsync_SetsUpdatedDateAndStatus()
        {
            CategoryDTO originalDto = new CategoryDTO { Id = 1 };
            CategoryDTO newDto = new CategoryDTO { Id = 1, CategoryName = "Updated", Description = "X" };
            Category existingEntity = new Category { Id = 1, CategoryName = "Old", Description = "Y", Status = DataStatus.Inserted };

            _validatorMock.Setup(v => v.ValidateAsync(newDto, default))
                .ReturnsAsync(new ValidationResult());

            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingEntity);
            _mapperMock.Setup(m => m.Map(newDto, existingEntity));
            _categoryRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            await _manager.UpdateAsync(originalDto, newDto);

            existingEntity.Status.Should().Be(DataStatus.Updated);
            existingEntity.UpdatedDate.Should().NotBeNull();
            existingEntity.UpdatedDate!.Value.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
        }

        #endregion

        #region SoftDeleteByIdAsync

        [Fact]
        public async Task SoftDeleteByIdAsync_WhenFound_ReturnsSuccess()
        {
            Category entity = new Category { Id = 1, CategoryName = "Test", Description = "X", Status = DataStatus.Inserted };

            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _categoryRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            Result result = await _manager.SoftDeleteByIdAsync(1);

            result.IsSuccess.Should().BeTrue();
            entity.Status.Should().Be(DataStatus.Deleted);
            entity.DeletionDate.Should().NotBeNull();
        }

        [Fact]
        public async Task SoftDeleteByIdAsync_WhenNotFound_ReturnsNotFound()
        {
            _categoryRepoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null!);

            Result result = await _manager.SoftDeleteByIdAsync(999);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.NotFound);
        }

        #endregion

        #region HardDeleteByIdAsync

        [Fact]
        public async Task HardDeleteByIdAsync_WhenSoftDeleted_ReturnsSuccess()
        {
            Category entity = new Category { Id = 1, CategoryName = "Test", Description = "X", Status = DataStatus.Deleted };

            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _categoryRepoMock.Setup(r => r.HardDeleteAsync(entity)).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync(default)).ReturnsAsync(1);

            Result result = await _manager.HardDeleteByIdAsync(1);

            result.IsSuccess.Should().BeTrue();
            _categoryRepoMock.Verify(r => r.HardDeleteAsync(entity), Times.Once);
        }

        [Fact]
        public async Task HardDeleteByIdAsync_WhenNotSoftDeleted_ReturnsFailed()
        {
            Category entity = new Category { Id = 1, CategoryName = "Test", Description = "X", Status = DataStatus.Inserted };

            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            Result result = await _manager.HardDeleteByIdAsync(1);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.Failed);
            _categoryRepoMock.Verify(r => r.HardDeleteAsync(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task HardDeleteByIdAsync_WhenNotFound_ReturnsNotFound()
        {
            _categoryRepoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null!);

            Result result = await _manager.HardDeleteByIdAsync(999);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.NotFound);
        }

        #endregion

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_WhenFound_ReturnsDto()
        {
            Category entity = new Category { Id = 1, CategoryName = "Test", Description = "X" };
            CategoryDTO dto = new CategoryDTO { Id = 1, CategoryName = "Test", Description = "X" };

            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CategoryDTO>(entity)).Returns(dto);

            CategoryDTO result = await _manager.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.CategoryName.Should().Be("Test");
        }

        #endregion

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ReturnsMappedList()
        {
            List<Category> entities = new List<Category>
            {
                new Category { Id = 1, CategoryName = "A", Description = "X" },
                new Category { Id = 2, CategoryName = "B", Description = "Y" }
            };

            List<CategoryDTO> dtos = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 1, CategoryName = "A", Description = "X" },
                new CategoryDTO { Id = 2, CategoryName = "B", Description = "Y" }
            };

            _categoryRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(entities)).Returns(dtos);

            List<CategoryDTO> result = await _manager.GetAllAsync();

            result.Should().HaveCount(2);
        }

        #endregion

        #region GetActives / GetPassives

        [Fact]
        public async Task GetActives_ReturnsNonDeletedEntities()
        {
            List<Category> activeEntities = new List<Category>
            {
                new Category { Id = 1, CategoryName = "A", Description = "X", Status = DataStatus.Inserted }
            };

            List<CategoryDTO> dtos = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 1, CategoryName = "A", Description = "X" }
            };

            _categoryRepoMock.Setup(r => r.WhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(activeEntities);
            _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(activeEntities)).Returns(dtos);

            List<CategoryDTO> result = await _manager.GetActives();

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetPassives_ReturnsDeletedEntities()
        {
            List<Category> passiveEntities = new List<Category>
            {
                new Category { Id = 2, CategoryName = "B", Description = "Y", Status = DataStatus.Deleted }
            };

            List<CategoryDTO> dtos = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 2, CategoryName = "B", Description = "Y", Status = DataStatus.Deleted }
            };

            _categoryRepoMock.Setup(r => r.WhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(passiveEntities);
            _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(passiveEntities)).Returns(dtos);

            List<CategoryDTO> result = await _manager.GetPassives();

            result.Should().HaveCount(1);
        }

        #endregion

        #region GetPagedAsync

        [Fact]
        public async Task GetPagedAsync_ReturnsCorrectPagedResult()
        {
            List<Category> items = new List<Category>
            {
                new Category { Id = 1, CategoryName = "A", Description = "X" }
            };

            List<CategoryDTO> dtos = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 1, CategoryName = "A", Description = "X" }
            };

            _categoryRepoMock.Setup(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((items, 15));
            _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(items)).Returns(dtos);

            PagedResult<CategoryDTO> result = await _manager.GetPagedAsync(1, 10);

            result.Items.Should().HaveCount(1);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalCount.Should().Be(15);
            result.TotalPages.Should().Be(2);
        }

        [Fact]
        public async Task GetPagedAsync_WhenPageLessThan1_DefaultsTo1()
        {
            _categoryRepoMock.Setup(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((new List<Category>(), 0));
            _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(It.IsAny<List<Category>>()))
                .Returns(new List<CategoryDTO>());

            PagedResult<CategoryDTO> result = await _manager.GetPagedAsync(0, 10);

            result.CurrentPage.Should().Be(1);
        }

        [Fact]
        public async Task GetPagedAsync_WhenPageSizeOver100_CapsTo100()
        {
            _categoryRepoMock.Setup(r => r.GetPagedAsync(1, 100, It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((new List<Category>(), 0));
            _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(It.IsAny<List<Category>>()))
                .Returns(new List<CategoryDTO>());

            PagedResult<CategoryDTO> result = await _manager.GetPagedAsync(1, 200);

            result.PageSize.Should().Be(100);
        }

        #endregion
    }
}
