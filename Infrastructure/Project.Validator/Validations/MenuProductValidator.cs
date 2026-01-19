using FluentValidation;
using Project.Application.DTOs;

public class MenuProductValidator : AbstractValidator<MenuProductDTO>
{
    public MenuProductValidator()
    {
        RuleFor(x => x.MenuId)
     .GreaterThan(0).WithMessage("Menü seçilmelidir.");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Ürün seçilmelidir.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");


        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Aktiflik durumu belirtilmelidir.");

    }
}