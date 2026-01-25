using FluentValidation;
using Project.Application.DTOs;
using Project.Domain.Enums;

namespace Project.Validator.Validations
{
    public class StockTransActionValidator : AbstractValidator<StockTransActionDTO>
    {
        public StockTransActionValidator()
        {
         
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("İşlem yapılacak ürün seçilmelidir.")
                .GreaterThan(0).WithMessage("Geçersiz ürün referansı.");

            // Miktar Kontrolü
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Miktar alanı boş bırakılamaz.")
                .NotEqual(0).WithMessage("İşlem miktarı sıfır olamaz.");

         
            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Birim fiyat negatif olamaz.");

          
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Geçersiz stok işlem tipi.");

            

           
            RuleFor(x => x.SupplierId)
                .NotEmpty().When(x => x.Type == TransActionType.Return) 
                .WithMessage("İade işlemlerinde tedarikçi seçilmesi zorunludur.");

            RuleFor(x => x.Description)
                .NotEmpty().When(x => x.Type == TransActionType.Waste) 
                .WithMessage("Zayi (kayıp) işlemlerinde açıklama girilmesi zorunludur.")
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden fazla olamaz.");
        }
    }
}