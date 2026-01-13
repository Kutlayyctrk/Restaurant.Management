using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class SupplierValidator:AbstractValidator<SupplierDTO>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.SupplierName)
    .NotEmpty().WithMessage("Tedarikçi adı zorunludur.")
    .MinimumLength(3).WithMessage("Tedarikçi adı en az 3 karakter olmalıdır.")
    .MaximumLength(100).WithMessage("Tedarikçi adı en fazla 100 karakter olmalıdır.")
    .Must(name => name == name?.Trim()).WithMessage("Tedarikçi adı başta veya sonda boşluk içeremez.");

            RuleFor(x => x.ContactName)
                .NotEmpty().WithMessage("İlgili kişi adı zorunludur.")
                .MaximumLength(100).WithMessage("İlgili kişi adı en fazla 100 karakter olmalıdır.")
                .Must(name => name == name?.Trim()).WithMessage("İlgili kişi adı başta veya sonda boşluk içeremez.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olmalıdır.")
                .Matches(@"^\+?[0-9\s\-\(\)]{7,20}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı zorunludur.")
                .MaximumLength(100).WithMessage("Email en fazla 100 karakter olmalıdır.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .Must(email => email == email?.Trim()).WithMessage("Email başta veya sonda boşluk içeremez.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres alanı zorunludur.")
                .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir.")
                .Must(a => a == a?.Trim()).WithMessage("Adres başta veya sonda boşluk içeremez.");
        }
    }
}
