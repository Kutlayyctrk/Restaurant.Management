using Microsoft.EntityFrameworkCore.Query;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class TableDTO : BaseDto
    {
        public string TableNumber { get; set; }
        public TableStatus TableStatus { get; set; }
    
        public int? WaiterId { get; set; }
    }
}
