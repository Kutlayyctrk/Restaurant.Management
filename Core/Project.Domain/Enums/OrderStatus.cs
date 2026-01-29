using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum OrderStatus
    {
       
        SentToKitchen,  // Garson onayladı, sipariş mutfağa düştü
        Completed,      // Sipariş garsona teslim edildi, mutfak için bitti
        Closed          // Sipariş tamamen kapandı (ödeme alındı, süreç bitti)

    }

}
