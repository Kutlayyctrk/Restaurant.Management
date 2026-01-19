using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class AppUserRoleDTO:BaseDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
    }
}
