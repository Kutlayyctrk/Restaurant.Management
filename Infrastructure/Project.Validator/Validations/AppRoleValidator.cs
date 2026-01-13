using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class AppRoleValidator:AbstractValidator<AppRoleDTO>
    {
        public AppRoleValidator()
        {
            
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Rol adı boş bırakılamaz.").MinimumLength(3).WithMessage("Rol adı en az 3 karakter olmalıdır.").MaximumLength(50).WithMessage("Rol adı en fazla 50 karakter olabilir.").MaximumLength(15).WithMessage("Rol Adı en fazla 15 karakter olabilir.").Must(x=>x==x.Trim()).WithMessage("Rol adı başında veya sonunda boşluk karakteri olamaz.");
            RuleFor(x=>x.Id).GreaterThan(0).WithMessage("Geçerli rol Id'si girilmeli.");
        }
    }
}
