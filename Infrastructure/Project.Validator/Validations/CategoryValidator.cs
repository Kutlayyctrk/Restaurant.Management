using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class CategoryValidator:AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName)
          .NotEmpty().WithMessage("Kategori adı zorunludur.")
          .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
          .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olmalıdır.")
          .Must(name => name == name?.Trim()).WithMessage("Kategori adı başta veya sonda boşluk içeremez.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olabilir.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description)); //eğer boş değilse kuralı koydum.

            When(x => x.ParentCategoryId.HasValue, () =>
            {
                RuleFor(x => x.ParentCategoryId.Value)
                    .GreaterThan(0).WithMessage("Üst kategori Id pozitif bir değer olmalıdır.");
                RuleFor(x => x)
                   .Must(dto => dto.ParentCategoryId != dto.Id)
                   .WithMessage("Kategori kendi kendisinin üst kategorisi olamaz.");
            });
        }
    }
}
