using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class AppUserDTO : BaseDto
    {
       
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? Password { get; set; }
        public bool? RememberMe { get; set; }
        public IList<int> RoleIds { get; set; }
       
    }
}
