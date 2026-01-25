using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum TransActionType
    {
        Purchase,   // Satın alma
        Sale,       // Satış
        Return,     // İade
        Waste,      // Zay
        Adjustment   // Sayım Farkı
    }
}
