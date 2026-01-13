using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class AppUserProfileValidator:AbstractValidator<AppUserProfileDTO>
    {
        public AppUserProfileValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("Ad boş bırakılamaz.").MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.").MaximumLength(25).WithMessage("Ad en fazla 25 karakter olabilir.").Must(x=>x==x.Trim()).WithMessage("Ad başında veya sonunda boşluk karakteri olamaz.");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("Soyad boş bırakılamaz.").MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.").MaximumLength(25).WithMessage("Soyad en fazla 25 karakter olabilir.").Must(x=>x==x.Trim()).WithMessage("Soyad başında veya sonunda boşluk karakteri olamaz.");
            RuleFor(x=>x.TCKNo).NotEmpty().WithMessage("TCKNo boş bırakılamaz.").Length(11).WithMessage("TCKNo 11 karakter olmalıdır.").Matches("^[0-9]{11}$").WithMessage("TCKNo sadece rakamlardan oluşmalıdır.");
            RuleFor(x=>x.Salary).GreaterThanOrEqualTo(0).WithMessage("Maaş negatif olamaz.");
            RuleFor(x=>x.BirthDate).LessThan(DateTime.Now).WithMessage("Doğum tarihi bugünden büyük olamaz.");
            RuleFor(x=>x.HireDate).LessThanOrEqualTo(DateTime.Now).WithMessage("İşe giriş tarihi bugünden büyük olamaz.").GreaterThanOrEqualTo(x=>x.BirthDate).WithMessage("İşe giriş tarihi doğum tarihinden küçük olamaz.");

           
        }
    }
}
