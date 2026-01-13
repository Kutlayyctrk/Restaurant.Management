using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class AppUserRoleValidator:AbstractValidator<AppUserRoleDTO>
    {
        public AppUserRoleValidator()
        {
            RuleFor(x => x.UserId)
             .GreaterThan(0).WithMessage("Geçerli bir kullanıcı Id'si girilmelidir.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Geçerli bir rol Id'si girilmelidir.");

        }
    }
}
