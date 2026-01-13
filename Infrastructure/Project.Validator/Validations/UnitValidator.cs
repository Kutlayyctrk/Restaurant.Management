using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class UnitValidator:AbstractValidator<UnitDTO>
    {
        public UnitValidator()
        {
            RuleFor(x => x.UnitName)
                .NotEmpty().WithMessage("Birim adı zorunludur.")
                .MinimumLength(2).WithMessage("Birim adı en az 2 karakter olmalıdır.")
                .MaximumLength(30).WithMessage("Birim adı en fazla 30 karakter olabilir.")
                .Must(name => name == name?.Trim()).WithMessage("Birim adı başta veya sonda boşluk içeremez.");

            When(x => !string.IsNullOrWhiteSpace(x.UnitAbbreviation), () =>
            {
                RuleFor(x => x.UnitAbbreviation)
                    .MaximumLength(10).WithMessage("Birim kısaltması en fazla 10 karakter olabilir.")
                    .Must(a => a == a?.Trim()).WithMessage("Birim kısaltması başta veya sonda boşluk içeremez.");
            });
        }
    }
}
