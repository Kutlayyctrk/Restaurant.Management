using FluentValidation;
using Project.Application.DTOs;

public class MenuValidator : AbstractValidator<MenuDTO>
{
    public MenuValidator()
    {
        RuleFor(x => x.MenuName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
    }
}
