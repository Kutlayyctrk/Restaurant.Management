using FluentValidation;
using Project.Application.DTOs;

public class AppUserValidator : AbstractValidator<AppUserDTO>
{
    public AppUserValidator()
    {
        // Register ve Update için kurallar
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
            .MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.")
            .MaximumLength(25).WithMessage("Kullanıcı adı en fazla 25 karakter olmalıdır.")
            .Must(name => name != null && name == name.Trim())
            .WithMessage("Kullanıcı adı başta veya sonda boşluk içeremez.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.")
            .Must(p => p == p.Trim()).WithMessage("Şifre başta veya sonda boşluk içeremez.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email alanı zorunludur.")
            .MaximumLength(30).WithMessage("Email alanı en fazla 30 karakter olmalıdır.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

     
        RuleSet("Register", () =>
        {
            RuleFor(x => x.ConfirmEmail)
                .NotEmpty().WithMessage("Email tekrar alanı zorunludur.")
                .Equal(x => x.Email).WithMessage("Email alanları uyuşmuyor.");
        });

        // Login için kuralla
        RuleSet("Login", () =>
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                .MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.")
                .MaximumLength(25).WithMessage("Kullanıcı adı en fazla 25 karakter olmalıdır.")
                .Must(name => name != null && name == name.Trim())
                .WithMessage("Kullanıcı adı başta veya sonda boşluk içeremez.");
        });
    }
}