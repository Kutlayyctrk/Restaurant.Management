using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class RecipeItemValidator:AbstractValidator<RecipeItemDTO>
    {
        public RecipeItemValidator()
        {
            RuleFor(x => x.ProductId)
               .GreaterThan(0).WithMessage("Geçerli bir Ürün Id'si girilmelidir.");

            RuleFor(x => x.UnitId)
                .GreaterThan(0).WithMessage("Geçerli bir Birim Id'si girilmelidir.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0m).WithMessage("Miktar 0'dan büyük olmalıdır.");

            When(x => !string.IsNullOrWhiteSpace(x.ProductName), () =>
            {
                RuleFor(x => x.ProductName)
                    .MaximumLength(150).WithMessage("Ürün adı en fazla 150 karakter olmalıdır.")
                    .Must(name => name == name.Trim()).WithMessage("Ürün adı başta veya sonda boşluk içeremez.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.UnitName), () =>
            {
                RuleFor(x => x.UnitName)
                    .MaximumLength(50).WithMessage("Birim adı en fazla 50 karakter olmalıdır.")
                    .Must(name => name == name.Trim()).WithMessage("Birim adı başta veya sonda boşluk içeremez.");
            });
        }
    }
}
