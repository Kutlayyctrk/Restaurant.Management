using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class StockTransAction : BaseEntity
    {
        public int ProductId { get; set; }
        public int? SupplierId { get; set; } //İade işlemi için
        public int AppUserId { get; set; } //İşlemi yapan kişi

        public decimal Quantity { get; set; } //ürün için miktar
        public DateTime? InvoiceDate { get; set; } //Satın alma  ve  iade işlemi için fatura tarihi
        public string TransActionType { get; set; }//işlem adı(iade, satış, zayi vb)
        public string? Description { get; set; } //iade sebebi zayi sebebi girilebilir.

        //Relational Properties

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual AppUser User { get; set; }

    }
}
