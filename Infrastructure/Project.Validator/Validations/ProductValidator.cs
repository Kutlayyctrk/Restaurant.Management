using FluentValidation;
using Project.Application.DTOs;

namespace Project.Validator.Validations
{
    public class ProductValidator : AbstractValidator<ProductDTO>
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
                .GreaterThan(0).WithMessage("Geçerli bir birim seçilmelidir.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");

            // İş kuralları:
            RuleFor(x => x.IsSellable)
                .Equal(true).When(x => x.IsExtra)
                .WithMessage("Extra ürünler satılabilir olmalıdır.");

            RuleFor(x => x.CanBeProduced)
                .Equal(false).When(x => x.IsReadyMade)
                .WithMessage("Hazır ürünler üretilemez.");
        }
    }
}