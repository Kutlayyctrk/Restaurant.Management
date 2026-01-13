using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class OrderValidator:AbstractValidator<OrderDTO>
    {
        public OrderValidator(IValidator<OrderDetailDTO> orderDetailValidator)
        {
            RuleFor(x => x.TableId)
          .GreaterThan(0).WithMessage("Geçerli bir masa Id'si girilmelidir.");

            RuleFor(x => x.WaiterId)
                .GreaterThan(0).When(x => x.WaiterId.HasValue).WithMessage("Geçerli bir garson Id'si girilmelidir.");

            RuleFor(x => x.OrderDetails)
                .NotNull().WithMessage("Sipariş detayı en az 1 satır olmalıdır.")
                .Must(x => x != null && x.Count > 0).WithMessage("Sipariş detayı en az 1 satır olmalıdır.");

            RuleForEach(x => x.OrderDetails)
                .NotNull()
                .SetValidator(orderDetailValidator);
        }
    }
}
