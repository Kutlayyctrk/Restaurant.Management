using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class AppUserValidator:AbstractValidator<AppUserDTO>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.UserName)
              .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
              .MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.")
              .MaximumLength(25).WithMessage("Kullanıcı adı en fazla 25 karakter olmalıdır.")
              .Must(name => name == name.Trim()).WithMessage("Kullanıcı adı başta veya sonda boşluk içeremez.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı zorunludur.")
                .MaximumLength(30).WithMessage("Email alanı en fazla 30 karakter olmalıdır.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.ConfirmedEmail)
                .NotEmpty().WithMessage("Email tekrar alanı zorunludur.")
                .Equal(x => x.Email).WithMessage("Email alanları uyuşmuyor.");

         
        }
    }
}
