using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum OrderStatus
    {
        Pending,        // Garson siparişi aldı, henüz mutfağa gönderilmedi
        SentToKitchen,  // Garson onayladı, sipariş mutfağa düştü
        InChefQueue,    // Mutfak şefi siparişi gördü, hazırlık aşamasında
        Ready,          // Sipariş hazırlandı, garsona teslim edilmek üzere
        Completed,      // Sipariş garsona teslim edildi, mutfak için bitti
        Closed          // Sipariş tamamen kapandı (ödeme alındı, süreç bitti)

    }

}
