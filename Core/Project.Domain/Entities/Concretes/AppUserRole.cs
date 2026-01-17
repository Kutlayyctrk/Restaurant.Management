using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities.Abstract;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class AppUserRole : IdentityUserRole<int>, IEntity
    {

       
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DataStatus Status { get; set; }

        //Relational Properties

        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
