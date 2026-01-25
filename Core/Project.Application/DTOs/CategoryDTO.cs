using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class CategoryDTO:BaseDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }

    }
}
