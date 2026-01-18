using FluentValidation;
using Project.Application.DTOs;

namespace Project.Validator.Validations
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailDTO>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Geçerli bir Ürün Id'si girilmelidir.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0m).WithMessage("Birim fiyat negatif olamaz.");

            RuleFor(x => x.DetailState)
        .IsInEnum().WithMessage("Geçerli bir sipariş detayı durumu seçilmelidir.");

        }
    }
}