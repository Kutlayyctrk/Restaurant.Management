using FluentValidation;
using Project.Application.DTOs;
using Project.Domain.Enums;

namespace Project.Validator.Validations
{
    public class OrderValidator : AbstractValidator<OrderDTO>
    {
        public OrderValidator(IValidator<OrderDetailDTO> orderDetailValidator)
        {
            RuleFor(x => x.TableId)
     .GreaterThan(0)
     .When(x => x.Type == OrderType.Sale)
     .WithMessage("Geçerli bir masa Id'si girilmelidir (sadece satış faturası için).");

            RuleFor(x => x.WaiterId)
                .GreaterThan(0).When(x => x.WaiterId.HasValue)
                .WithMessage("Geçerli bir garson Id'si girilmelidir.");

            RuleFor(x => x.SupplierId)
                .GreaterThan(0).When(x => x.SupplierId.HasValue)
                .WithMessage("Geçerli bir tedarikçi Id'si girilmelidir.");

            RuleFor(x => x.OrderDetails)
                .NotNull().WithMessage("Sipariş detayı en az 1 satır olmalıdır.")
                .Must(x => x != null && x.Count > 0)
                .WithMessage("Sipariş detayı en az 1 satır olmalıdır.");

            RuleForEach(x => x.OrderDetails)
                .NotNull()
                .SetValidator(orderDetailValidator);

            RuleFor(x => x.OrderState)
                .IsInEnum().WithMessage("Geçerli bir sipariş durumu seçilmelidir.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Geçerli bir fatura tipi seçilmelidir.");
        }
    }
}