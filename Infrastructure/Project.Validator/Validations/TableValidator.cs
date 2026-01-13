using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class TableValidator:AbstractValidator<TableDTO>
    {
        public TableValidator()
        {
            RuleFor(x => x.TableNumber)
               .NotEmpty().WithMessage("Masa numarası zorunludur.")
               .MinimumLength(1).WithMessage("Masa numarası en az 1 karakter olmalıdır.")
               .MaximumLength(20).WithMessage("Masa numarası en fazla 20 karakter olabilir.")
               .Must(s => s == s?.Trim()).WithMessage("Masa numarası başta veya sonda boşluk içeremez.");

            When(x => x.WaiterId.HasValue, () =>
            {
                RuleFor(x => x.WaiterId.Value)
                    .GreaterThan(0).WithMessage("WaiterId pozitif bir değer olmalıdır.");
            });
        }
    }
}
