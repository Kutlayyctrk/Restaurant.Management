using FluentAssertions;
using Project.Application.Enums;
using Project.Application.Results;
using Xunit;

namespace Project.UnitTests
{
    public class ResultTests
    {
        [Fact]
        public void Succeed_ReturnsIsSuccessTrue()
        {
            Result result = Result.Succeed("OK");

            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(OperationStatus.Success);
            result.Message.Should().Be("OK");
        }

        [Fact]
        public void Succeed_WithoutMessage_ReturnsIsSuccessTrue()
        {
            Result result = Result.Succeed();

            result.IsSuccess.Should().BeTrue();
            result.Message.Should().BeNull();
        }

        [Fact]
        public void Failure_WithMessage_ReturnsIsSuccessFalse()
        {
            Result result = Result.Failure(OperationStatus.NotFound, "Bulunamadý");

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.NotFound);
            result.Message.Should().Be("Bulunamadý");
        }

        [Fact]
        public void Failure_WithErrors_ReturnsJoinedMessage()
        {
            List<string> errors = new List<string> { "Hata1", "Hata2" };

            Result result = Result.Failure(OperationStatus.ValidationError, errors);

            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(OperationStatus.ValidationError);
            result.Errors.Should().HaveCount(2);
            result.Message.Should().Contain("Hata1");
            result.Message.Should().Contain("Hata2");
        }

        [Fact]
        public void GenericResult_Succeed_ContainsData()
        {
            Result<int> result = Result<int>.Succeed(42, "Veri bulundu");

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(42);
            result.Message.Should().Be("Veri bulundu");
        }

        [Fact]
        public void GenericResult_Failure_DataIsDefault()
        {
            Result<int> result = Result<int>.Failure(OperationStatus.Failed, "Hata");

            result.IsSuccess.Should().BeFalse();
            result.Data.Should().Be(default);
        }

        [Fact]
        public void GenericResult_Failure_WithErrors()
        {
            List<string> errors = new List<string> { "E1", "E2" };

            Result<string> result = Result<string>.Failure(OperationStatus.ValidationError, errors);

            result.IsSuccess.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Errors.Should().HaveCount(2);
        }
    }

    public class PagedResultTests
    {
        [Fact]
        public void TotalPages_CalculatesCorrectly()
        {
            PagedResult<string> result = new PagedResult<string>
            {
                Items = new List<string> { "a", "b" },
                CurrentPage = 1,
                PageSize = 2,
                TotalCount = 5
            };

            result.TotalPages.Should().Be(3);
        }

        [Fact]
        public void HasPreviousPage_FirstPage_ReturnsFalse()
        {
            PagedResult<string> result = new PagedResult<string>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalCount = 30
            };

            result.HasPreviousPage.Should().BeFalse();
        }

        [Fact]
        public void HasPreviousPage_SecondPage_ReturnsTrue()
        {
            PagedResult<string> result = new PagedResult<string>
            {
                CurrentPage = 2,
                PageSize = 10,
                TotalCount = 30
            };

            result.HasPreviousPage.Should().BeTrue();
        }

        [Fact]
        public void HasNextPage_LastPage_ReturnsFalse()
        {
            PagedResult<string> result = new PagedResult<string>
            {
                CurrentPage = 3,
                PageSize = 10,
                TotalCount = 30
            };

            result.HasNextPage.Should().BeFalse();
        }

        [Fact]
        public void HasNextPage_NotLastPage_ReturnsTrue()
        {
            PagedResult<string> result = new PagedResult<string>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalCount = 30
            };

            result.HasNextPage.Should().BeTrue();
        }

        [Fact]
        public void TotalPages_WhenTotalCountZero_ReturnsZero()
        {
            PagedResult<string> result = new PagedResult<string>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalCount = 0
            };

            result.TotalPages.Should().Be(0);
        }
    }
}
