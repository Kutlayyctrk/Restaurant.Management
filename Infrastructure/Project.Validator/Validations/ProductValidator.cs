using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class ProductValidator:AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {

            RuleFor(x => x.ProductName)
               .NotEmpty().WithMessage("Ürün adı zorunludur.")
               .MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır.")
               .MaximumLength(150).WithMessage("Ürün adı en fazla 150 karakter olmalıdır.")
               .Must(name => name == name?.Trim()).WithMessage("Ürün adı başta veya sonda boşluk içeremez.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0m).WithMessage("Birim fiyat negatif olamaz.");

            RuleFor(x => x.UnitId)
                .GreaterThan(0).WithMessage("Birim Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori Id pozitif bir değer olmalıdır.");
        }
    }
}
