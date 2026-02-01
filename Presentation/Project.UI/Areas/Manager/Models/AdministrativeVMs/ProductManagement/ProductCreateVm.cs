using System.ComponentModel.DataAnnotations;

public class ProductCreateVm
{
    [Required(ErrorMessage = "Ürün adı zorunludur.")]
    public string ProductName { get; set; }
    [Required(ErrorMessage = "Birim fiyat zorunludur.")]
    public decimal UnitPrice { get; set; }
    [Required(ErrorMessage = "Birim seçimi zorunludur.")]
    public int UnitId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Kategori seçimi zorunludur.")]
    public int CategoryId { get; set; }
    public bool IsSellable { get; set; }
    public bool IsExtra { get; set; }
    public bool CanBeProduced { get; set; }
    public bool IsReadyMade { get; set; }
}