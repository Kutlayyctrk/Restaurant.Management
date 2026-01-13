using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class RecipeValidator:AbstractValidator<RecipeDTO>
    {
        public RecipeValidator(IValidator<RecipeItemDTO> itemValidator)
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Geçerli bir ProductId girilmelidir.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir CategoryId girilmelidir.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Açıklama en fazla 1000 karakter olabilir.");

            RuleFor(x => x.RecipeItem)
                .NotNull().WithMessage("Reçete içeriği boş olamaz.")
                .Must(list => list != null && list.Count > 0).WithMessage("En az bir reçete maddesi eklemelisiniz.")
                .Must(list => list == null || list.Select(i => i.ProductId).Distinct().Count() == list.Count)
                .WithMessage("Reçete maddeleri içinde aynı üründen yalnızca bir kere olmalıdır.");

            RuleForEach(x => x.RecipeItem)
                .NotNull()
                .SetValidator(itemValidator);
        }
    }
}
