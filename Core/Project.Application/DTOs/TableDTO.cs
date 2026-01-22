using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class TableDTO:BaseDto
    {
        public string TableNumber { get; set; }
        public string TableName { get; set; }
        public int? WaiterId { get; set; }
    }
}
