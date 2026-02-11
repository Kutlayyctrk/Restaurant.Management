using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Project.Application.DTOs;
using Project.Validator.Validations;
using Xunit;

namespace Project.UnitTests
{
    public class AppRoleValidatorTests
    {
        private readonly AppRoleValidator _validator = new();

        [Fact]
        public void Valid_Role_Passes() =>
            _validator.Validate(new AppRoleDTO { Name = "Admin" }).IsValid.Should().BeTrue();

        [Fact]
        public void EmptyName_Fails() =>
            _validator.Validate(new AppRoleDTO { Name = "" }).IsValid.Should().BeFalse();

        [Fact]
        public void ShortName_Fails() =>
            _validator.Validate(new AppRoleDTO { Name = "AB" }).IsValid.Should().BeFalse();

        [Fact]
        public void LongName_Fails() =>
            _validator.Validate(new AppRoleDTO { Name = new string('A', 51) }).IsValid.Should().BeFalse();

        [Fact]
        public void NameWithTrailingSpace_Fails() =>
            _validator.Validate(new AppRoleDTO { Name = "Admin " }).IsValid.Should().BeFalse();

        [Fact]
        public void NameWithLeadingSpace_Fails() =>
            _validator.Validate(new AppRoleDTO { Name = " Admin" }).IsValid.Should().BeFalse();
    }

    public class AppUserValidatorTests
    {
        private readonly AppUserValidator _validator = new();

        [Fact]
        public void Valid_User_Passes()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "test@test.com" };
            _validator.Validate(dto).IsValid.Should().BeTrue();
        }

        [Fact]
        public void EmptyUserName_Fails()
        {
            var dto = new AppUserDTO { UserName = "", Email = "test@test.com" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShortUserName_Fails()
        {
            var dto = new AppUserDTO { UserName = "ab", Email = "test@test.com" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongUserName_Fails()
        {
            var dto = new AppUserDTO { UserName = new string('a', 26), Email = "test@test.com" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void UserNameWithSpace_Fails()
        {
            var dto = new AppUserDTO { UserName = " testuser", Email = "test@test.com" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyEmail_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void InvalidEmail_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "notanemail" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongEmail_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = new string('a', 25) + "@b.com" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void PasswordWithLeadingSpace_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = " Pass1234!" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ValidPassword_Passes()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = "Pass1234!" };
            _validator.Validate(dto).IsValid.Should().BeTrue();
        }

        [Fact]
        public void PasswordNoUppercase_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = "pass1234!" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void PasswordNoLowercase_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = "PASS1234!" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void PasswordNoDigit_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = "Password!" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void PasswordNoSpecialChar_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = "Pass1234a" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShortPassword_Fails()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = "Pa1!" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NullPassword_Passes()
        {
            var dto = new AppUserDTO { UserName = "testuser", Email = "t@t.com", Password = null };
            _validator.Validate(dto).IsValid.Should().BeTrue();
        }
    }

    public class AppUserProfileValidatorTests
    {
        private readonly AppUserProfileValidator _validator = new();

        private AppUserProfileDTO ValidProfile() => new()
        {
            FirstName = "Ali",
            LastName = "Veli",
            TCKNo = "12345678901",
            Salary = 5000m,
            BirthDate = new DateTime(1990, 1, 1),
            HireDate = DateTime.Now.AddDays(-1)
        };

        [Fact]
        public void Valid_Profile_Passes() =>
            _validator.Validate(ValidProfile()).IsValid.Should().BeTrue();

        [Fact]
        public void EmptyFirstName_Fails()
        {
            var dto = ValidProfile(); dto.FirstName = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShortFirstName_Fails()
        {
            var dto = ValidProfile(); dto.FirstName = "A";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongFirstName_Fails()
        {
            var dto = ValidProfile(); dto.FirstName = new string('A', 26);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void FirstNameWithSpace_Fails()
        {
            var dto = ValidProfile(); dto.FirstName = " Ali";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyLastName_Fails()
        {
            var dto = ValidProfile(); dto.LastName = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShortLastName_Fails()
        {
            var dto = ValidProfile(); dto.LastName = "A";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongLastName_Fails()
        {
            var dto = ValidProfile(); dto.LastName = new string('A', 26);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyTCKNo_Fails()
        {
            var dto = ValidProfile(); dto.TCKNo = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShortTCKNo_Fails()
        {
            var dto = ValidProfile(); dto.TCKNo = "1234567890";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NonNumericTCKNo_Fails()
        {
            var dto = ValidProfile(); dto.TCKNo = "1234567890A";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NegativeSalary_Fails()
        {
            var dto = ValidProfile(); dto.Salary = -1;
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void FutureBirthDate_Fails()
        {
            var dto = ValidProfile(); dto.BirthDate = DateTime.Now.AddDays(1);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void HireDateBeforeBirthDate_Fails()
        {
            var dto = ValidProfile();
            dto.BirthDate = new DateTime(2000, 1, 1);
            dto.HireDate = new DateTime(1999, 1, 1);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }
    }

    public class AppUserRoleValidatorTests
    {
        private readonly AppUserRoleValidator _validator = new();

        [Fact]
        public void Valid_Passes() =>
            _validator.Validate(new AppUserRoleDTO { UserId = 1, RoleId = 1 }).IsValid.Should().BeTrue();

        [Fact]
        public void ZeroUserId_Fails() =>
            _validator.Validate(new AppUserRoleDTO { UserId = 0, RoleId = 1 }).IsValid.Should().BeFalse();

        [Fact]
        public void ZeroRoleId_Fails() =>
            _validator.Validate(new AppUserRoleDTO { UserId = 1, RoleId = 0 }).IsValid.Should().BeFalse();

        [Fact]
        public void NegativeUserId_Fails() =>
            _validator.Validate(new AppUserRoleDTO { UserId = -1, RoleId = 1 }).IsValid.Should().BeFalse();

        [Fact]
        public void NegativeRoleId_Fails() =>
            _validator.Validate(new AppUserRoleDTO { UserId = 1, RoleId = -1 }).IsValid.Should().BeFalse();
    }

    public class SupplierValidatorTests
    {
        private readonly SupplierValidator _validator = new();

        private SupplierDTO ValidSupplier() => new()
        {
            SupplierName = "Test Tedarik",
            ContactName = "Ali Veli",
            PhoneNumber = "+905551234567",
            Email = "test@test.com",
            Address = "Test Mah. No:1"
        };

        [Fact]
        public void Valid_Passes() =>
            _validator.Validate(ValidSupplier()).IsValid.Should().BeTrue();

        [Fact]
        public void EmptyName_Fails()
        {
            var dto = ValidSupplier(); dto.SupplierName = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShortName_Fails()
        {
            var dto = ValidSupplier(); dto.SupplierName = "AB";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongName_Fails()
        {
            var dto = ValidSupplier(); dto.SupplierName = new string('A', 101);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NameWithSpace_Fails()
        {
            var dto = ValidSupplier(); dto.SupplierName = " Test";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyContactName_Fails()
        {
            var dto = ValidSupplier(); dto.ContactName = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongContactName_Fails()
        {
            var dto = ValidSupplier(); dto.ContactName = new string('A', 101);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyPhone_Fails()
        {
            var dto = ValidSupplier(); dto.PhoneNumber = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void InvalidPhone_Fails()
        {
            var dto = ValidSupplier(); dto.PhoneNumber = "abc";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyEmail_Fails()
        {
            var dto = ValidSupplier(); dto.Email = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void InvalidEmail_Fails()
        {
            var dto = ValidSupplier(); dto.Email = "notanemail";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyAddress_Fails()
        {
            var dto = ValidSupplier(); dto.Address = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongAddress_Fails()
        {
            var dto = ValidSupplier(); dto.Address = new string('A', 501);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void AddressWithSpace_Fails()
        {
            var dto = ValidSupplier(); dto.Address = " Test";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }
    }

    public class UnitValidatorTests
    {
        private readonly UnitValidator _validator = new();

        [Fact]
        public void Valid_Passes() =>
            _validator.Validate(new UnitDTO { UnitName = "Kilogram" }).IsValid.Should().BeTrue();

        [Fact]
        public void EmptyName_Fails() =>
            _validator.Validate(new UnitDTO { UnitName = "" }).IsValid.Should().BeFalse();

        [Fact]
        public void ShortName_Fails() =>
            _validator.Validate(new UnitDTO { UnitName = "A" }).IsValid.Should().BeFalse();

        [Fact]
        public void LongName_Fails() =>
            _validator.Validate(new UnitDTO { UnitName = new string('A', 31) }).IsValid.Should().BeFalse();

        [Fact]
        public void NameWithSpace_Fails() =>
            _validator.Validate(new UnitDTO { UnitName = " Kg" }).IsValid.Should().BeFalse();

        [Fact]
        public void ValidAbbreviation_Passes() =>
            _validator.Validate(new UnitDTO { UnitName = "Kilogram", UnitAbbreviation = "kg" }).IsValid.Should().BeTrue();

        [Fact]
        public void LongAbbreviation_Fails() =>
            _validator.Validate(new UnitDTO { UnitName = "Kilogram", UnitAbbreviation = new string('A', 11) }).IsValid.Should().BeFalse();

        [Fact]
        public void AbbreviationWithSpace_Fails() =>
            _validator.Validate(new UnitDTO { UnitName = "Kilogram", UnitAbbreviation = " kg" }).IsValid.Should().BeFalse();
    }

    public class MenuValidatorTests
    {
        private readonly MenuValidator _validator = new();

        [Fact]
        public void Valid_Passes()
        {
            var dto = new MenuDTO { MenuName = "Yaz Menusu", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) };
            _validator.Validate(dto).IsValid.Should().BeTrue();
        }

        [Fact]
        public void EmptyName_Fails()
        {
            var dto = new MenuDTO { MenuName = "", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongName_Fails()
        {
            var dto = new MenuDTO { MenuName = new string('A', 101), StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void StartDateAfterEndDate_Fails()
        {
            var dto = new MenuDTO { MenuName = "Test", StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }
    }

    public class MenuProductValidatorTests
    {
        private readonly MenuProductValidator _validator = new();

        [Fact]
        public void Valid_Passes()
        {
            var dto = new MenuProductDTO { MenuId = 1, ProductId = 1, UnitPrice = 10m, IsActive = true };
            _validator.Validate(dto).IsValid.Should().BeTrue();
        }

        [Fact]
        public void ZeroMenuId_Fails()
        {
            var dto = new MenuProductDTO { MenuId = 0, ProductId = 1, UnitPrice = 10m, IsActive = true };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroProductId_Fails()
        {
            var dto = new MenuProductDTO { MenuId = 1, ProductId = 0, UnitPrice = 10m, IsActive = true };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NegativePrice_Fails()
        {
            var dto = new MenuProductDTO { MenuId = 1, ProductId = 1, UnitPrice = -1m, IsActive = true };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }
    }

    public class RecipeItemValidatorTests
    {
        private readonly RecipeItemValidator _validator = new();

        [Fact]
        public void Valid_Passes()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 1, Quantity = 5m };
            _validator.Validate(dto).IsValid.Should().BeTrue();
        }

        [Fact]
        public void ZeroProductId_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 0, UnitId = 1, Quantity = 5m };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroUnitId_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 0, Quantity = 5m };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroQuantity_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 1, Quantity = 0m };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongProductName_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 1, Quantity = 1m, ProductName = new string('A', 151) };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ProductNameWithSpace_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 1, Quantity = 1m, ProductName = " Test" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongUnitName_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 1, Quantity = 1m, UnitName = new string('A', 51) };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void UnitNameWithSpace_Fails()
        {
            var dto = new RecipeItemDTO { ProductId = 1, UnitId = 1, Quantity = 1m, UnitName = " Kg" };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }
    }

    public class RecipeValidatorTests
    {
        private readonly RecipeValidator _validator;

        public RecipeValidatorTests()
        {
            _validator = new RecipeValidator(new RecipeItemValidator());
        }

        private RecipeDTO ValidRecipe() => new()
        {
            Name = "Test Tarif",
            ProductId = 1,
            CategoryId = 1,
            RecipeItems = new List<RecipeItemDTO>
            {
                new() { ProductId = 1, UnitId = 1, Quantity = 2m }
            }
        };

        [Fact]
        public void Valid_Passes() =>
            _validator.Validate(ValidRecipe()).IsValid.Should().BeTrue();

        [Fact]
        public void EmptyName_Fails()
        {
            var dto = ValidRecipe(); dto.Name = "";
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongName_Fails()
        {
            var dto = ValidRecipe(); dto.Name = new string('A', 51);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroProductId_Fails()
        {
            var dto = ValidRecipe(); dto.ProductId = 0;
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void ZeroCategoryId_Fails()
        {
            var dto = ValidRecipe(); dto.CategoryId = 0;
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongDescription_Fails()
        {
            var dto = ValidRecipe(); dto.Description = new string('A', 1001);
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NullRecipeItems_Fails()
        {
            var dto = ValidRecipe(); dto.RecipeItems = null;
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyRecipeItems_Fails()
        {
            var dto = ValidRecipe(); dto.RecipeItems = new List<RecipeItemDTO>();
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void DuplicateProductInItems_Fails()
        {
            var dto = ValidRecipe();
            dto.RecipeItems = new List<RecipeItemDTO>
            {
                new() { ProductId = 1, UnitId = 1, Quantity = 1m },
                new() { ProductId = 1, UnitId = 2, Quantity = 2m }
            };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }

        [Fact]
        public void InvalidRecipeItem_Fails()
        {
            var dto = ValidRecipe();
            dto.RecipeItems = new List<RecipeItemDTO>
            {
                new() { ProductId = 0, UnitId = 0, Quantity = 0m }
            };
            _validator.Validate(dto).IsValid.Should().BeFalse();
        }
    }
}
