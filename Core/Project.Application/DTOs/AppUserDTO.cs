using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class AppUserDTO : BaseDto
    {
        public string Email { get; set; }
        public string ConfirmedEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DataStatus Status { get; set; }
    }
}
