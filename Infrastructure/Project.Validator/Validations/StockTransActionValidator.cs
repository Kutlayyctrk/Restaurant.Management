using FluentValidation;
using Project.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.Validations
{
    public class StockTransActionValidator : AbstractValidator<StockTransActionDTO>
    {
        public StockTransActionValidator()
        {
            RuleFor(x => x.ProductId)
               .GreaterThan(0).WithMessage("Ürün Id pozitif olmalıdır.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0m).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.TransActionType)
                .NotEmpty().WithMessage("Stok hareket tipi boş olamaz.")
                .MaximumLength(30).WithMessage("Stok hareket tipi en fazla 30 karakter olabilir.")
                .Must(t => t == t?.Trim()).WithMessage("Stok hareket tipi başta veya sonda boşluk içeremez.");
            RuleFor(x => x.InvoiceDate)
    .NotNull().WithMessage("Fatura tarihi zorunludur.");

            When(x => x.SupplierId.HasValue, () =>
            {
                RuleFor(x => x.SupplierId.Value)
                    .GreaterThan(0).WithMessage("SupplierId pozitif bir değer olmalıdır.");
            });

            When(x => x.AppUserId.HasValue, () =>
            {
                RuleFor(x => x.AppUserId.Value)
                    .GreaterThan(0).WithMessage("AppUserId pozitif bir değer olmalıdır.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                    .Must(d => d == d?.Trim()).WithMessage("Açıklama başta veya sonda boşluk içeremez.");
            });
            When(x => !string.IsNullOrWhiteSpace(x.TransActionType) &&
          (x.TransActionType.Contains("Satınalma") || x.TransActionType.Contains("İade")),
    () =>
    {
        RuleFor(x => x.SupplierId)
            .NotNull().WithMessage("Satınalma/İade işlemlerinde tedarikçi zorunludur.");
    });
        }
    }
}
