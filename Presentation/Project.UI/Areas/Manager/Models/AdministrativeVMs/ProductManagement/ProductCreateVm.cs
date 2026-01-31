using System.ComponentModel.DataAnnotations;

public class ProductCreateVm
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int UnitId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Kategori seçimi zorunludur.")]
    public int CategoryId { get; set; }
    public bool IsSellable { get; set; }
    public bool IsExtra { get; set; }
    public bool CanBeProduced { get; set; }
    public bool IsReadyMade { get; set; }
}